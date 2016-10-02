namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Network.RequestProcessors;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an HTML script element.
    /// http://www.w3.org/TR/html5/scripting-1.html#execute-the-script-block
    /// </summary>
    sealed class HtmlScriptElement : HtmlElement, IHtmlScriptElement
    {
        #region Fields

        private readonly Boolean _parserInserted;
        private readonly ScriptRequestProcessor _request;

        private Boolean _started;
        private Boolean _forceAsync;

        #endregion

        #region ctor

        public HtmlScriptElement(Document owner, String prefix = null, Boolean parserInserted = false, Boolean started = false)
            : base(owner, TagNames.Script, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
            _forceAsync = false;
            _started = started;
            _parserInserted = parserInserted;
            _request = ScriptRequestProcessor.Create(this);
        }

        #endregion

        #region Properties

        public IDownload CurrentDownload
        {
            get { return _request?.Download; }
        }

        public String Source
        {
            get { return this.GetOwnAttribute(AttributeNames.Src); }
            set { this.SetOwnAttribute(AttributeNames.Src, value); }
        }

        public String Type
        {
            get { return this.GetOwnAttribute(AttributeNames.Type); }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        public String CharacterSet
        {
            get { return this.GetOwnAttribute(AttributeNames.Charset); }
            set { this.SetOwnAttribute(AttributeNames.Charset, value); }
        }

        public String Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        public String CrossOrigin
        {
            get { return this.GetOwnAttribute(AttributeNames.CrossOrigin); }
            set { this.SetOwnAttribute(AttributeNames.CrossOrigin, value); }
        }

        public Boolean IsDeferred
        {
            get { return this.GetBoolAttribute(AttributeNames.Defer); }
            set { this.SetBoolAttribute(AttributeNames.Defer, value); }
        }

        public Boolean IsAsync
        {
            get { return this.GetBoolAttribute(AttributeNames.Async); }
            set { this.SetBoolAttribute(AttributeNames.Async, value); }
        }

        public String Integrity
        {
            get { return this.GetOwnAttribute(AttributeNames.Integrity); }
            set { this.SetOwnAttribute(AttributeNames.Integrity, value); }
        }

        #endregion

        #region Internal Methods

        protected override void OnParentChanged()
        {
            base.OnParentChanged();

            if (!_parserInserted && Prepare(Owner))
            {
                RunAsync(CancellationToken.None);
            }
        }
        
        internal Task RunAsync(CancellationToken cancel)
        {
            return _request?.RunAsync(cancel);
        }

        /// <summary>
        /// More information available at:
        /// http://www.w3.org/TR/html5/scripting-1.html#prepare-a-script
        /// </summary>
        internal Boolean Prepare(Document document)
        {
            var options = document.Options;
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
                _forceAsync = !IsAsync;
            }

            if (String.IsNullOrEmpty(src) && String.IsNullOrEmpty(Text))
            {
                return false;
            }
            else if (_request.Engine == null)
            {
                return false;
            }
            else if (wasParserInserted)
            {
                _forceAsync = false;
            }

            _started = true;

            if (!String.IsNullOrEmpty(eventAttr) && !String.IsNullOrEmpty(forAttr))
            {
                eventAttr = eventAttr.Trim();
                forAttr = forAttr.Trim();

                if (eventAttr.EndsWith("()"))
                {
                    eventAttr = eventAttr.Substring(0, eventAttr.Length - 2);
                }

                var isWindow = forAttr.Equals(AttributeNames.Window, StringComparison.OrdinalIgnoreCase);
                var isLoadEvent = eventAttr.Equals("onload", StringComparison.OrdinalIgnoreCase);

                if (!isWindow || !isLoadEvent)
                {
                    return false;
                }
            }

            if (src != null)
            {
                if (src.Length != 0)
                {
                    return InvokeLoadingScript(document, this.HyperReference(src));
                }

                document.QueueTask(FireErrorEvent);
            }
            else
            {
                _request.Process(Text);
                return true;
            }

            return false;
        }

        #endregion

        #region Methods

        public override INode Clone(Boolean deep = true)
        {
            var node = new HtmlScriptElement(Owner, Prefix, _parserInserted, _started)
            {
                _forceAsync = _forceAsync
            };
            CloneElement(node, deep);
            return node;
        }

        #endregion

        #region Helpers

        Boolean InvokeLoadingScript(Document document, Url url)
        {
            var executeDirectly = true;

            //Just add to the (end of) set of scripts
            if (_parserInserted && (IsDeferred || IsAsync))
            {
                document.AddScript(this);
                executeDirectly = false;
            }

            this.Process(_request, url);
            return executeDirectly;
        }

        void FireErrorEvent()
        {
            this.FireSimpleEvent(EventNames.Error);
        }

        #endregion
    }
}
