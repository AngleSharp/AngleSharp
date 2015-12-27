namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Services.Scripting;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an HTML script element.
    /// http://www.w3.org/TR/html5/scripting-1.html#execute-the-script-block
    /// </summary>
    sealed class HtmlScriptElement : HtmlElement, IHtmlScriptElement
    {
        #region Fields

        readonly Boolean _parserInserted;

        Boolean _started;
        Boolean _forceAsync;
        Action _runScript;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML script element.
        /// </summary>
        public HtmlScriptElement(Document owner, String prefix = null, Boolean parserInserted = false, Boolean started = false)
            : base(owner, Tags.Script, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
            _forceAsync = false;
            _started = started;
            _parserInserted = parserInserted;
        }

        #endregion

        #region Internal Properties

        internal String AlternativeLanguage
        {
            get
            {
                var language = this.GetOwnAttribute(AttributeNames.Language);
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
            get { return this.GetOwnAttribute(AttributeNames.Src); }
            set { this.SetOwnAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the type of an embedded resource.
        /// </summary>
        public String Type
        {
            get { return this.GetOwnAttribute(AttributeNames.Type); }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets the character encoding of the external script resource.
        /// </summary>
        public String CharacterSet
        {
            get { return this.GetOwnAttribute(AttributeNames.Charset); }
            set { this.SetOwnAttribute(AttributeNames.Charset, value); }
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
            get { return this.GetOwnAttribute(AttributeNames.CrossOrigin); }
            set { this.SetOwnAttribute(AttributeNames.CrossOrigin, value); }
        }

        /// <summary>
        /// Gets or sets if the script should be deferred.
        /// </summary>
        public Boolean IsDeferred
        {
            get { return this.HasOwnAttribute(AttributeNames.Defer); }
            set { this.SetOwnAttribute(AttributeNames.Defer, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if script should execute asynchronously.
        /// </summary>
        public Boolean IsAsync
        {
            get { return this.HasOwnAttribute(AttributeNames.Async); }
            set { this.SetOwnAttribute(AttributeNames.Async, value ? String.Empty : null); }
        }

        #endregion

        #region Internal Methods
        
        internal void Run()
        {
            if (_runScript != null)
            {
                var cancelled = this.FireSimpleEvent(EventNames.BeforeScriptExecute, cancelable: true);

                if (!cancelled)
                {
                    _runScript();
                }
            }
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
            else if (Engine == null)
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
                if (_parserInserted && document.GetStyleSheetDownloads().Any())
                {
                    _runScript = RunFromSource;
                    return true;
                }

                RunFromSource();
            }

            return false;
        }

        #endregion

        #region Helpers

        Boolean InvokeLoadingScript(Document document, Url url)
        {
            var fromParser = true;

            //Just add to the (end of) set of scripts
            if (!_parserInserted || IsDeferred || IsAsync)
            {
                document.AddScript(this);
                fromParser = false;
            }

            var request = this.CreateRequestFor(url);
            var task = LoadScriptAsync(document.Loader, request);
            document.DelayLoad(task);
            return fromParser;
        }

        async Task LoadScriptAsync(IResourceLoader loader, ResourceRequest request)
        {
            var setting = CrossOrigin.ToEnum(CorsSetting.None);
            var behavior = OriginBehavior.Taint;
            var response = await loader.FetchWithCorsAsync(request, setting, behavior).ConfigureAwait(false);
            var completion = new TaskCompletionSource<Boolean>();
            _runScript = () =>
            {
                RunFromResponse(response);
                response.Dispose();
                completion.SetResult(true);
            };
            await completion.Task.ConfigureAwait(false);
        }

        void RunFromResponse(IResponse response)
        {
            Eval(options => Engine.Evaluate(response, options));
            FireLoadEvent();
        }

        void RunFromSource()
        {
            Eval(options => Engine.Evaluate(Text, options));
            Owner.QueueTask(FireLoadEvent);
        }

        void Eval(Action<ScriptOptions> evaluateWith)
        {
            var options = CreateOptions();
            var document = Owner;
            var insert = document.Source.Index;

            try { evaluateWith(options); }
            catch { /* We omit failed 3rd party services */ }

            document.Source.Index = insert;
            FireAfterScriptExecuteEvent();
        }

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
            var document = Owner;
            var context = document != null ? document.DefaultView : null;

            return new ScriptOptions
            {
                Context = context,
                Document = document,
                Element = this,
                Encoding = TextEncoding.Resolve(CharacterSet)
            };
        }

        #endregion
    }
}
