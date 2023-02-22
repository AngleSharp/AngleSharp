namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Io;
    using AngleSharp.Scripting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class CallbackScriptEngine : IScriptingService
    {
        public CallbackScriptEngine(Action<ScriptOptions> callback, String type = null)
        {
            Callback = callback;
            Type = type ?? "c-sharp";
        }

        public String Type { get; }

        public Boolean SupportsType(String mimeType)
        {
            return mimeType.Equals(Type, StringComparison.OrdinalIgnoreCase);
        }

        public Action<ScriptOptions> Callback
        {
            get;
            private set;
        }

        public Task EvaluateScriptAsync(IResponse response, ScriptOptions options, CancellationToken cancel)
        {
            Callback?.Invoke(options);
            return Task.FromResult(true);
        }
    }
}
