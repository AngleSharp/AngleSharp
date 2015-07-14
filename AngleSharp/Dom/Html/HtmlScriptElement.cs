namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Services.Scripting;
    using System;

    /// <summary>
    /// Represents an HTML script element.
    /// </summary>
    sealed class HtmlScriptElement : HtmlElement, IHtmlScriptElement
    {
        #region Fields

        Boolean _started;
        Boolean _parserInserted;
        Boolean _wasParserInserted;
        Boolean _forceAsync;
        Boolean _readyToBeExecuted;
        IResponse _current;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML script element.
        /// </summary>
        public HtmlScriptElement(Document owner, String prefix = null)
            : base(owner, Tags.Script, prefix, NodeFlags.Special | NodeFlags.LiteralText)
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
                var language = GetOwnAttribute(AttributeNames.Language);
                var type = Type ?? (language != null ? "text/" + language : null);
                return String.IsNullOrEmpty(type) ? MimeTypes.DefaultJavaScript : type;
            }
        }

        /// <summary>
        /// Gets or sets athe address of the resource.
        /// </summary>
        public String Source
        {
            get { return GetOwnAttribute(AttributeNames.Src); }
            set { SetOwnAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the type of an embedded resource.
        /// </summary>
        public String Type
        {
            get { return GetOwnAttribute(AttributeNames.Type); }
            set { SetOwnAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets the character encoding of the external script resource.
        /// </summary>
        public String CharacterSet
        {
            get { return GetOwnAttribute(AttributeNames.Charset); }
            set { SetOwnAttribute(AttributeNames.Charset, value); }
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
            get { return GetOwnAttribute(AttributeNames.CrossOrigin); }
            set { SetOwnAttribute(AttributeNames.CrossOrigin, value); }
        }

        /// <summary>
        /// Gets or sets if the script should be deferred.
        /// </summary>
        public Boolean IsDeferred
        {
            get { return GetOwnAttribute(AttributeNames.Defer) != null; }
            set { SetOwnAttribute(AttributeNames.Defer, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if script should execute asynchronously.
        /// </summary>
        public Boolean IsAsync
        {
            get { return GetOwnAttribute(AttributeNames.Async) != null; }
            set { SetOwnAttribute(AttributeNames.Async, value ? String.Empty : null); }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// More information available at:
        /// http://www.w3.org/TR/html5/scripting-1.html#execute-the-script-block
        /// </summary>
        internal void Run()
        {
            if (_current == null)
            {
                Error();
            }
            else if (!CancelledBeforeScriptExecute())
            {
                var owner = Owner;
                var engine = owner.Options.GetScriptEngine(ScriptLanguage);

                try { engine.Evaluate(_current, CreateOptions()); }
                catch { /* We omit failed 3rd party services */ }
                finally { _current.Dispose(); }

                AfterScriptExecute();

                if (Source != null) 
                    Load();
                else 
                    owner.QueueTask(Load);
            }
        }

        /// <summary>
        /// More information available at:
        /// http://www.w3.org/TR/html5/scripting-1.html#prepare-a-script
        /// </summary>
        internal void Prepare()
        {
            if (_started || Owner == null)
                return;

            var options = Owner.Options;
            var engine = options.GetScriptEngine(ScriptLanguage);

            if (engine == null)
                return;

            _wasParserInserted = _parserInserted;
            _parserInserted = false;

            _forceAsync = _wasParserInserted && !IsAsync;

            if (String.IsNullOrEmpty(Source) && String.IsNullOrEmpty(Text))
                return;

            if (_wasParserInserted)
            {
                _parserInserted = true;
                _forceAsync = false;
            }

            _started = true;

            var eventAttr = GetOwnAttribute(AttributeNames.Event);
            var forAttr = GetOwnAttribute(AttributeNames.For);

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

                InvokeLoadingScript(this.HyperReference(src));
            }
            else if (_parserInserted && Owner.HasScriptBlockingStyleSheet())
            {
                _readyToBeExecuted = true;
            }
            else
            {
                engine.Evaluate(Text, CreateOptions());
            }
        }

        void InvokeLoadingScript(Url url)
        {
            var owner = Owner;

            if (_parserInserted && !IsAsync)
            {
                if (IsDeferred)
                    owner.AddScript(this);
            }
            else if (!IsAsync && !_forceAsync)
            {
                //Add to end of list of scripts (in order) --> sufficient
                owner.AddScript(this);
            }
            else
            {
                //Just add to the set of scripts
                owner.AddScript(this);
            }

            var request = this.CreateRequestFor(url);
            var setting = CrossOrigin.ToEnum(CorsSetting.None);
            var behavior = OriginBehavior.Taint;
            this.CreateTask(c => Owner.Loader.FetchWithCorsAsync(request, setting, behavior, c)).ContinueWith(m =>
            {
                _current = m.Result;
                _readyToBeExecuted = (_parserInserted && !IsAsync) || _readyToBeExecuted;
            });
        }

        void Load()
        {
            this.FireSimpleEvent(EventNames.Load);
        }

        void Error()
        {
            this.FireSimpleEvent(EventNames.Error);
        }

        Boolean CancelledBeforeScriptExecute()
        {
            return this.FireSimpleEvent(EventNames.BeforeScriptExecute, cancelable: true);
        }

        void AfterScriptExecute()
        {
            this.FireSimpleEvent(EventNames.AfterScriptExecute, bubble: true);
        }

        ScriptOptions CreateOptions()
        {
            return new ScriptOptions
            {
                Context = Owner.DefaultView,
                Document = Owner,
                Element = this,
                Encoding = TextEncoding.Resolve(CharacterSet)
            };
        }

        #endregion
    }
}
