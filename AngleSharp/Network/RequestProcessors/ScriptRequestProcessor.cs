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

        #endregion

        #region ctor

        private ScriptRequestProcessor(HtmlScriptElement script, IResourceLoader loader)
        {
            _script = script;
            _document = script.Owner;
            _loader = loader;
        }

        internal static ScriptRequestProcessor Create(HtmlScriptElement script)
        {
            var document = script.Owner;
            var loader = document.Loader;
            return new ScriptRequestProcessor(script, loader);
        }

        #endregion

        #region Properties

        public IDownload Download 
        {
            get { return _download; }
        }

        #endregion

        #region Methods

        public async Task RunAsync(CancellationToken cancel)
        {
            if (_response != null)
            {
                var cancelled = _script.FireSimpleEvent(EventNames.BeforeScriptExecute, cancelable: true);

                if (!cancelled)
                {
                    var options = CreateOptions();
                    var engine = _script.Engine;

                    if (engine != null)
                    {
                        var insert = _document.Source.Index;

                        try
                        {
                            await engine.EvaluateScriptAsync(_response, options, cancel).ConfigureAwait(false);
                        }
                        catch
                        {
                            /* We omit failed 3rd party services */
                        }

                        _document.Source.Index = insert;
                        FireAfterScriptExecuteEvent();
                    }

                    _document.QueueTask(FireLoadEvent);
                    _response.Dispose();
                    _response = null;
                }
            }
        }

        public void Process(String content)
        {
            _response = VirtualResponse.Create(res => res.Content(content).Address(_script.BaseUri));
        }

        public async Task Process(ResourceRequest request)
        {
            if (_loader != null)
            {
                var setting = _script.CrossOrigin.ToEnum(CorsSetting.None);
                var behavior = OriginBehavior.Taint;
                _download = await _loader.FetchWithCorsAsync(request, setting, behavior).ConfigureAwait(false);
                _response = await _download.Task.ConfigureAwait(false);
            }
        }

        ScriptOptions CreateOptions()
        {
            return new ScriptOptions
            {
                Context = _document.DefaultView,
                Document = _document,
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
