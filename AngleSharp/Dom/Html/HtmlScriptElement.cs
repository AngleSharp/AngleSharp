namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Services.Scripting;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents an HTML script element.
    /// http://www.w3.org/TR/html5/scripting-1.html#execute-the-script-block
    /// </summary>
    sealed class HtmlScriptElement : HtmlElement, IHtmlScriptElement
    {
        #region Fields

        Boolean _started;
        Boolean _parserInserted;
        Boolean _forceAsync;
        Action _runScript;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML script element.
        /// </summary>
        public HtmlScriptElement(Document owner, String prefix = null)
            : base(owner, Tags.Script, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
            _forceAsync = false;
            _parserInserted = false;
            _started = false;
        }

        #endregion

        #region Internal Properties

        internal String AlternativeLanguage
        {
            get
            {
                var language = GetOwnAttribute(AttributeNames.Language);
                return language != null ? "text/" + language : null;
            }
        }

        internal IScriptEngine Engine
        {
            get { return Owner.Options.GetScriptEngine(ScriptLanguage); }
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
                var type = Type ?? AlternativeLanguage;
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

        internal void SetStarted(Boolean fragmentCase)
        {
            _parserInserted = true;
            _started = fragmentCase;
        }
        
        internal void Run()
        {
            if (_runScript == null)
                return;

            var cancelled = this.FireSimpleEvent(EventNames.BeforeScriptExecute, cancelable: true);

            if (cancelled == false)
                _runScript();
        }

        void RunFromResponse(IResponse response)
        {
            var options = CreateOptions();

            try { Engine.Evaluate(response, options); }
            catch { /* We omit failed 3rd party services */ }

            FireAfterScriptExecuteEvent();
            FireLoadEvent();
        }

        void RunFromSource()
        {
            var options = CreateOptions();

            try { Engine.Evaluate(Text, options); }
            catch { /* We omit failed 3rd party services */ }

            FireAfterScriptExecuteEvent();
            Owner.QueueTask(FireLoadEvent);
        }

        /// <summary>
        /// More information available at:
        /// http://www.w3.org/TR/html5/scripting-1.html#prepare-a-script
        /// </summary>
        internal Boolean Prepare()
        {
            var options = Owner.Options;
            var eventAttr = GetOwnAttribute(AttributeNames.Event);
            var forAttr = GetOwnAttribute(AttributeNames.For);
            var src = Source;
            var wasParserInserted = _parserInserted;

            if (_started)
                return false;
            else if (wasParserInserted)
                _forceAsync = !IsAsync;

            _parserInserted = false;

            if (String.IsNullOrEmpty(src) && String.IsNullOrEmpty(Text))
                return false;
            else if (Engine == null)
                return false;
            else if (wasParserInserted) 
                _forceAsync = false;

            _parserInserted = true;
            _started = true;

            if (!String.IsNullOrEmpty(eventAttr) && !String.IsNullOrEmpty(forAttr))
            {
                eventAttr = eventAttr.Trim();
                forAttr = forAttr.Trim();

                if (eventAttr.EndsWith("()"))
                    eventAttr = eventAttr.Substring(0, eventAttr.Length - 2);

                if (!forAttr.Equals(AttributeNames.Window, StringComparison.OrdinalIgnoreCase) || 
                    !eventAttr.Equals("onload", StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            if (src != null)
            {
                if (src == String.Empty)
                    Owner.QueueTask(FireErrorEvent);
                else
                    return InvokeLoadingScript(this.HyperReference(src));
            }
            else if (_parserInserted && Owner.GetStyleSheetDownloads().Any())
            {
                _runScript = RunFromSource;
                return true;
            }
            else
            {
                RunFromSource();
            }

            return false;
        }

        Boolean InvokeLoadingScript(Url url)
        {
            var fromParser = true;

            //Just add to the (end of) set of scripts
            if ((_parserInserted && IsDeferred && !IsAsync) || !_parserInserted || IsAsync)
            {
                Owner.AddScript(this);
                fromParser = false;
            }

            var request = this.CreateRequestFor(url);
            var setting = CrossOrigin.ToEnum(CorsSetting.None);
            var behavior = OriginBehavior.Taint;
            this.CreateTask(async c =>
            {
                var response = await Owner.Loader.FetchWithCorsAsync(request, setting, behavior, c).ConfigureAwait(false);

                if (response != null)
                {
                    _runScript = () =>
                    {
                        RunFromResponse(response);
                        response.Dispose();
                    };
                }
                else
                    FireErrorEvent();

                return response;
            });

            return fromParser;
        }

        #endregion

        #region Helpers

        void FireLoadEvent()
        {
            this.FireSimpleEvent(EventNames.Load);
        }

        void FireErrorEvent()
        {
            this.FireSimpleEvent(EventNames.Error);
        }

        void FireAfterScriptExecuteEvent()
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
