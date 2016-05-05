namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Network;
    using AngleSharp.Services.Scripting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class CallbackScriptEngine : IScriptEngine
    {
        public CallbackScriptEngine(Action<ScriptOptions> callback, String type = null)
        {
            Callback = callback;
            Type = type ?? "c-sharp";
        }

        public String Type
        {
            get;
            private set;
        }

        public Action<ScriptOptions> Callback
        {
            get;
            private set;
        }

        public Task EvaluateScriptAsync(IResponse response, ScriptOptions options, CancellationToken cancel)
        {
            if (Callback != null)
            {
                Callback(options);
            }

            return Task.FromResult(true);
        }
    }
}
