namespace AngleSharp.Io.Processors
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Scripting;
    using AngleSharp.Text;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class ScriptRequestProcessor : IRequestProcessor
    {
        #region Fields

        private readonly IBrowsingContext _context;
        private readonly Document _document;
        private readonly HtmlScriptElement _script;
        private readonly IResourceLoader _loader;
        private IResponse _response;
        private IScriptingService _engine;

        #endregion

        #region ctor

        public ScriptRequestProcessor(IBrowsingContext context, HtmlScriptElement script)
        {
            _context = context;
            _document = script.Owner;
            _script = script;
            _loader = context.GetService<IResourceLoader>();
        }

        #endregion

        #region Properties

        public IDownload Download
        {
            get;
            private set;
        }

        public IScriptingService Engine => _engine ?? (_engine = _context.GetScripting(ScriptLanguage));

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
                catch
                {
                    await _document.QueueTaskAsync(FireErrorEvent).ConfigureAwait(false);
                }
            }

            if (_response != null)
            {
                var cancelled = await _document.QueueTaskAsync(FireBeforeScriptExecuteEvent).ConfigureAwait(false);

                if (!cancelled)
                {
                    var options = CreateOptions();
                    var insert = _document.Source.Index;

                    try
                    {
                        await _engine.EvaluateScriptAsync(_response, options, cancel).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        /* We omit failed 3rd party services */
                        _context.TrackError(ex);
                    }

                    _document.Source.Index = insert;
                    await _document.QueueTaskAsync(FireAfterScriptExecuteEvent).ConfigureAwait(false);
                    await _document.QueueTaskAsync(FireLoadEvent).ConfigureAwait(false);
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
                Download = _loader.FetchWithCorsAsync(new CorsRequest(request)
                {
                    Behavior = OriginBehavior.Taint,
                    Setting = _script.CrossOrigin.ToEnum(CorsSetting.None),
                    Integrity = _context.GetProvider<IIntegrityProvider>()
                });
                return Download.Task;
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Helpers

        private ScriptOptions CreateOptions() => new ScriptOptions(_document, _document.Loop)
        {
            Element = _script,
            Encoding = TextEncoding.Resolve(_script.CharacterSet)
        };

        private void FireLoadEvent(CancellationToken _) =>
            _script.FireSimpleEvent(EventNames.Load);

        private void FireErrorEvent(CancellationToken _) =>
            _script.FireSimpleEvent(EventNames.Error);

        private Boolean FireBeforeScriptExecuteEvent(CancellationToken _) =>
            _script.FireSimpleEvent(EventNames.BeforeScriptExecute, cancelable: true);

        private void FireAfterScriptExecuteEvent(CancellationToken _) =>
            _script.FireSimpleEvent(EventNames.AfterScriptExecute, bubble: true);

        #endregion
    }
}
