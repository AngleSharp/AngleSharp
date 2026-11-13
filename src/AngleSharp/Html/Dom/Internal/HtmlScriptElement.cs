namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Io.Processors;
    using AngleSharp.Text;

    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Construction;

    /// <summary>
    /// Represents an HTML script element.
    /// http://www.w3.org/TR/html5/scripting-1.html#execute-the-script-block
    /// </summary>
    sealed class HtmlScriptElement : HtmlElement, IHtmlScriptElement, IConstructableScriptElement
    {
        #region Fields

        private Boolean _parserInserted;
        private readonly ScriptRequestProcessor _request;

        private Boolean _started;
        private Boolean _forceAsync;

        #endregion

        #region ctor

        public HtmlScriptElement(Document owner, String? prefix = null, Boolean parserInserted = false, Boolean started = false)
            : base(owner, TagNames.Script, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
            _forceAsync = false;
            _started = started;
            _parserInserted = parserInserted;
            _request = new ScriptRequestProcessor(owner.Context, this);
        }

        #endregion

        #region Properties

        public IDownload? CurrentDownload => _request?.Download;

        public Boolean IsStarted => _started;

        public String? Source
        {
            get => this.GetOwnAttribute(AttributeNames.Src);
            set => this.SetOwnAttribute(AttributeNames.Src, value);
        }

        public String? Type
        {
            get => this.GetOwnAttribute(AttributeNames.Type);
            set => this.SetOwnAttribute(AttributeNames.Type, value);
        }

        public String? CharacterSet
        {
            get => this.GetOwnAttribute(AttributeNames.Charset);
            set => this.SetOwnAttribute(AttributeNames.Charset, value);
        }

        public String Text
        {
            get => TextContent;
            set => TextContent = value;
        }

        public String? CrossOrigin
        {
            get => this.GetOwnAttribute(AttributeNames.CrossOrigin);
            set => this.SetOwnAttribute(AttributeNames.CrossOrigin, value);
        }

        public Boolean IsDeferred
        {
            get => this.GetBoolAttribute(AttributeNames.Defer);
            set => this.SetBoolAttribute(AttributeNames.Defer, value);
        }

        public Boolean IsAsync
        {
            get => this.GetBoolAttribute(AttributeNames.Async);
            set => this.SetBoolAttribute(AttributeNames.Async, value);
        }

        public String? Integrity
        {
            get => this.GetOwnAttribute(AttributeNames.Integrity);
            set => this.SetOwnAttribute(AttributeNames.Integrity, value);
        }

        #endregion

        #region Methods

        public override Node Clone(Document owner, Boolean deep)
        {
            var node = new HtmlScriptElement(owner, Prefix, _parserInserted, _started);
            CloneElement(node, owner, deep);
            node._forceAsync = _forceAsync;
            return node;
        }

        #endregion

        #region Internal Methods

        protected override void OnParentChanged()
        {
            base.OnParentChanged();
            PrepareInserted();
        }

        protected override void NodeIsInserted(Node newNode)
        {
            base.NodeIsInserted(newNode);

            // a script inserted without text runs once it gets some
            if (!ReferenceEquals(newNode, this))
            {
                PrepareInserted();
            }
        }

        /// <summary>
        /// Prepares a connected script that has not started yet once it gets a source.
        /// </summary>
        internal void UpdateSource(String? value)
        {
            if (value is not null)
            {
                PrepareInserted();
            }
        }

        /// <summary>
        /// Prepares a script inserted by other means than the parser, but not one being removed.
        /// </summary>
        private void PrepareInserted()
        {
            if (!_parserInserted && Parent is not null && Prepare(Owner))
            {
                RunAsync(CancellationToken.None);
            }
        }

        internal Task RunAsync(CancellationToken cancel)
        {
            return _request?.RunAsync(cancel)!;
        }

        /// <summary>
        /// More information available at:
        /// http://www.w3.org/TR/html5/scripting-1.html#prepare-a-script
        /// </summary>
        internal Boolean Prepare(Document document)
        {
            var eventAttr = this.GetOwnAttribute(AttributeNames.Event);
            var forAttr = this.GetOwnAttribute(AttributeNames.For);
            var src = Source;
            var wasParserInserted = _parserInserted;

            if (_started)
            {
                return false;
            }
            else if (wasParserInserted)
            {
                // the parser only gets one try: a script that does not start now (e.g.,
                // one of another type) starts like an inserted one, e.g., once it gets a source
                _parserInserted = false;
                _forceAsync = !IsAsync;
            }

            if (String.IsNullOrEmpty(src) && String.IsNullOrEmpty(Text))
            {
                return false;
            }
            else if (_request.Engine is null)
            {
                return false;
            }
            else if (wasParserInserted)
            {
                _forceAsync = false;
            }

            _started = true;

            // Only after being marked as started: such a script does not run either once
            // it is moved to a document with scripting enabled.
            if (document.IsScriptingDisabled)
            {
                return false;
            }

            if (eventAttr is { Length: > 0 } && forAttr is { Length: >0 })
            {
                eventAttr = eventAttr.Trim();
                forAttr = forAttr.Trim();

                if (eventAttr.EndsWith("()"))
                {
                    eventAttr = eventAttr.Substring(0, eventAttr.Length - 2);
                }

                var isWindow = forAttr.Isi(AttributeNames.Window);
                var isLoadEvent = eventAttr.Isi("onload");

                if (!isWindow || !isLoadEvent)
                {
                    return false;
                }
            }

            if (src != null)
            {
                if (src.Length != 0)
                {
                    return InvokeLoadingScript(document, this.HyperReference(src)!, wasParserInserted);
                }

                document.QueueTask(FireErrorEvent);
            }
            else
            {
                _request.Process(Text);

                // a module of the parser is deferred, even when it is written inline
                if (wasParserInserted && IsModule)
                {
                    document.AddScript(this);
                    return false;
                }

                return true;
            }

            return false;
        }

        #endregion

        #region Helpers

        private Boolean InvokeLoadingScript(Document document, Url url, Boolean parserInserted)
        {
            var executeDirectly = true;

            //Just add to the (end of) set of scripts
            // a module is deferred without being marked as such
            if (parserInserted && (IsDeferred || IsAsync || IsModule))
            {
                document.AddScript(this);
                executeDirectly = false;
            }

            this.Process(_request, url);
            return executeDirectly;
        }

        private Boolean IsModule => Type?.Trim().Isi("module") == true;

        private void FireErrorEvent() => Owner.QueueTask(() => this.FireSimpleEvent(EventNames.Error));

        #endregion

        #region Construction

        Task IConstructableScriptElement.RunAsync(CancellationToken cancel)
        {
            return RunAsync(cancel);
        }

        Boolean IConstructableScriptElement.Prepare(IConstructableDocument document)
        {
            return Prepare((Document)document);
        }

        #endregion
    }
}
