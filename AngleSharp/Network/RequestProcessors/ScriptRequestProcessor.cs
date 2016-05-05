namespace AngleSharp.Network.RequestProcessors
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Services.Scripting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class ScriptRequestProcessor : IRequestProcessor
    {
        #region Fields

        readonly HtmlScriptElement _script;
        readonly Document _document;
        readonly IResourceLoader _loader;
        IDownload _download;
        IResponse _response;
        IScriptEngine _engine;

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
            get { return _download; }
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
            if (_download != null)
            {
                _response = await _download.Task.ConfigureAwait(false);
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
                var setting = _script.CrossOrigin.ToEnum(CorsSetting.None);
                var behavior = OriginBehavior.Taint;
                _download = _loader.FetchWithCors(request, setting, behavior);
                return _download.Task;
            }

            return null;
        }

        ScriptOptions CreateOptions()
        {
            return new ScriptOptions(_document)
            {
                Element = _script,
                Encoding = TextEncoding.Resolve(_script.CharacterSet)
            };
        }

        void FireLoadEvent()
        {
            _script.FireSimpleEvent(EventNames.Load);
        }

        void FireAfterScriptExecuteEvent()
        {
            _script.FireSimpleEvent(EventNames.AfterScriptExecute, bubble: true);
        }

        #endregion
    }
}
