namespace AngleSharp.Network.RequestProcessors
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Services;
    using AngleSharp.Services.Scripting;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class ScriptRequestProcessor : IRequestProcessor
    {
        #region Fields

        private readonly HtmlScriptElement _script;
        private readonly Document _document;
        private readonly IResourceLoader _loader;
        private IResponse _response;
        private IScriptEngine _engine;

        #endregion

        #region ctor

        private ScriptRequestProcessor(HtmlScriptElement script, Document document, IResourceLoader loader)
        {
            _script = script;
            _document = document;
            _loader = loader;
        }

        internal static ScriptRequestProcessor Create(HtmlScriptElement script)
        {
            var document = script.Owner;
            var loader = document.Loader;
            return new ScriptRequestProcessor(script, document, loader);
        }

        #endregion

        #region Properties

        public IDownload Download 
        {
            get;
            private set;
        }

        public IScriptEngine Engine
        {
            get { return _engine ?? (_engine = _document.Options.GetScriptEngine(ScriptLanguage)); }
        }

        public String AlternativeLanguage
        {
            get
            {
                var language = _script.GetOwnAttribute(AttributeNames.Language);
                return language != null ? "text/" + language : null;
            }
        }

        public String ScriptLanguage
        {
            get
            {
                var type = _script.Type ?? AlternativeLanguage;
                return String.IsNullOrEmpty(type) ? MimeTypeNames.DefaultJavaScript : type;
            }
        }

        #endregion

        #region Methods

        public async Task RunAsync(CancellationToken cancel)
        {
            var download = Download;

            if (download != null)
            {
                try
                {
                    _response = await download.Task.ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    FireErrorEvent();
                }
            }

            if (_response != null)
            {
                var cancelled = _script.FireSimpleEvent(EventNames.BeforeScriptExecute, cancelable: true);

                if (!cancelled)
                {
                    var options = CreateOptions();
                    var insert = _document.Source.Index;

                    try
                    {
                        await _engine.EvaluateScriptAsync(_response, options, cancel).ConfigureAwait(false);
                    }
                    catch
                    {
                        /* We omit failed 3rd party services */
                    }

                    _document.Source.Index = insert;
                    FireAfterScriptExecuteEvent();

                    _document.QueueTask(FireLoadEvent);
                    _response.Dispose();
                    _response = null;
                }
            }
        }

        public void Process(String content)
        {
            if (Engine != null)
            {
                _response = VirtualResponse.Create(res => res.Content(content).Address(_script.BaseUri));
            }
        }

        public Task ProcessAsync(ResourceRequest request)
        {
            if (_loader != null && Engine != null)
            {
                Download = _loader.FetchWithCors(new CorsRequest(request)
                {
                    Behavior = OriginBehavior.Taint,
                    Setting = _script.CrossOrigin.ToEnum(CorsSetting.None),
                    Integrity = _document.Options.GetProvider<IIntegrityProvider>()
                });
                return Download.Task;
            }

            return null;
        }

        #endregion

        #region Helpers

        private ScriptOptions CreateOptions()
        {
            return new ScriptOptions(_document)
            {
                Element = _script,
                Encoding = TextEncoding.Resolve(_script.CharacterSet)
            };
        }

        private void FireLoadEvent()
        {
            _script.FireSimpleEvent(EventNames.Load);
        }

        private void FireErrorEvent()
        {
            _script.FireSimpleEvent(EventNames.Error);
        }

        private void FireAfterScriptExecuteEvent()
        {
            _script.FireSimpleEvent(EventNames.AfterScriptExecute, bubble: true);
        }

        #endregion
    }
}
