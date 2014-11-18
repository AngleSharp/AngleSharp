namespace AngleSharp.DOM.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Infrastructure;
    using AngleSharp.Network;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an HTML script element.
    /// </summary>
    sealed class HTMLScriptElement : HTMLElement, IHtmlScriptElement
    {
        #region Fields

        Boolean _started;
        Boolean _parserInserted;
        Boolean _wasParserInserted;
        Boolean _forceAsync;
        Boolean _readyToBeExecuted;
        Task<IResponse> _load;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML script element.
        /// </summary>
        public HTMLScriptElement()
            : base(Tags.Script, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }

        #endregion

        #region Internal Properties

        internal Boolean IsReady
        {
            get { return _readyToBeExecuted; }
        }

        internal Boolean IsParserInserted 
        {
            get { return _parserInserted; }
            set { _parserInserted = value; }
        }

        internal Boolean IsAlreadyStarted
        {
            get { return _started; }
            set { _started = value; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the language of the script.
        /// </summary>
        public String ScriptLanguage
        {
            get
            {
                var type = Type ?? (GetAttribute(AttributeNames.Language) != null ? "text/" + GetAttribute(AttributeNames.Language) : null);

                if (String.IsNullOrEmpty(type))
                    return MimeTypes.DefaultJavaScript;

                return type;
            }
        }

        /// <summary>
        /// Gets or sets athe address of the resource.
        /// </summary>
        public String Source
        {
            get { return GetAttribute(AttributeNames.Src); }
            set { SetAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the type of an embedded resource.
        /// </summary>
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets the character encoding of the external script resource.
        /// </summary>
        public String CharacterSet
        {
            get { return GetAttribute(AttributeNames.Charset); }
            set { SetAttribute(AttributeNames.Charset, value); }
        }

        /// <summary>
        /// Gets or sets the text in the script element.
        /// </summary>
        public String Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        /// <summary>
        /// Gets or sets how the element handles crossorigin requests.
        /// </summary>
        public String CrossOrigin
        {
            get { return GetAttribute(AttributeNames.CrossOrigin); }
            set { SetAttribute(AttributeNames.CrossOrigin, value); }
        }

        /// <summary>
        /// Gets or sets if the script should be deferred.
        /// </summary>
        public Boolean IsDeferred
        {
            get { return GetAttribute(AttributeNames.Defer) != null; }
            set { SetAttribute(AttributeNames.Defer, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if script should execute asynchronously.
        /// </summary>
        public Boolean IsAsync
        {
            get { return GetAttribute(AttributeNames.Async) != null; }
            set { SetAttribute(AttributeNames.Async, value ? String.Empty : null); }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// More information available at:
        /// http://www.w3.org/TR/html5/scripting-1.html#execute-the-script-block
        /// </summary>
        internal void Run()
        {
            if (_load == null)
                return;

            if (_load.Exception != null || _load.IsFaulted)
            {
                Error();
                return;
            }

            if (CancelledBeforeScriptExecute())
                return;

            using (var result = _load.Result)
                Owner.Options.RunScript(result, CreateOptions(), ScriptLanguage);

            AfterScriptExecute();

            if (Source != null) Load();
            else Owner.QueueTask(Load);
        }

        /// <summary>
        /// More information available at:
        /// http://www.w3.org/TR/html5/scripting-1.html#prepare-a-script
        /// </summary>
        internal void Prepare()
        {
            var options = Owner.Options;

            if (_started)
                return;

            _wasParserInserted = _parserInserted;
            _parserInserted = false;

            _forceAsync = _wasParserInserted && !IsAsync;

            if ((String.IsNullOrEmpty(Source) && String.IsNullOrEmpty(Text)) || Owner == null)
                return;

            if (options.GetScriptEngine(ScriptLanguage) == null)
                return;

            if (_wasParserInserted)
            {
                _parserInserted = true;
                _forceAsync = false;
            }

            _started = true;

            if (!Owner.Options.IsScripting)
                return;

            var eventAttr = GetAttribute(AttributeNames.Event);
            var forAttr = GetAttribute(AttributeNames.For);

            if (!String.IsNullOrEmpty(eventAttr) && !String.IsNullOrEmpty(forAttr))
            {
                eventAttr = eventAttr.Trim();
                forAttr = forAttr.Trim();

                if (eventAttr.EndsWith("()"))
                    eventAttr = eventAttr.Substring(0, eventAttr.Length - 2);

                if (!forAttr.Equals(AttributeNames.Window, StringComparison.OrdinalIgnoreCase) || !eventAttr.Equals("onload", StringComparison.OrdinalIgnoreCase))
                    return;
            }

            var src = Source;

            if (src != null)
            {
                if (src == String.Empty)
                {
                    Owner.QueueTask(Error);
                    return;
                }

                var url = HyperRef(src);
                var requester = options.GetRequester(url.Scheme);

                if (requester == null)
                    return;

                _load = requester.LoadWithCorsAsync(url, CrossOrigin.ToEnum(CorsSetting.None), Owner.Origin, OriginBehavior.Taint);

                if (_parserInserted && !IsAsync)
                {
                    if (IsDeferred)
                        Owner.AddScript(this);

                    _load.ContinueWith(task => _readyToBeExecuted = true);
                }
                else if (!IsAsync && !_forceAsync)
                {
                    //The element must be added to the end of the list of scripts that will execute in order as soon as possible associated
                    //with the Document of the script element at the time the prepare a script algorithm started.
                    Owner.AddScript(this);
                }
                else
                {
                    //The element must be added to the set of scripts that will execute as soon as possible of the Document of the
                    //script element at the time the prepare a script algorithm started.
                    Owner.AddScript(this);
                }
            }
            else if (_parserInserted)
            {
                //and either the parser that created the script is an XML parser or it's an HTML parser whose script nesting level is
                //not greater than one, and the Document of the HTML parser or XML parser that created the script element has a style
                //sheet that is blocking scripts
                _readyToBeExecuted = true;
                //TODO do not run immediately, i.e. remove the following line
                options.RunScript(Text, CreateOptions(), ScriptLanguage);
            }
            else
            {
                options.RunScript(Text, CreateOptions(), ScriptLanguage);
            }
        }

        void Load()
        {
            FireSimpleEvent(EventNames.Load);
        }

        void Error()
        {
            FireSimpleEvent(EventNames.Error);
        }

        Boolean CancelledBeforeScriptExecute()
        {
            return FireSimpleEvent(EventNames.BeforeScriptExecute, cancelable: true);
        }

        void AfterScriptExecute()
        {
            FireSimpleEvent(EventNames.AfterScriptExecute, bubble: true);
        }

        ScriptOptions CreateOptions()
        {
            return new ScriptOptions
            {
                Context = Owner.DefaultView,
                Document = Owner,
                Element = this,
                Encoding = DocumentEncoding.Resolve(CharacterSet)
            };
        }

        #endregion
    }
}
