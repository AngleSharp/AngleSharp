namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Browser;
    using AngleSharp.Io;
    using AngleSharp.Scripting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Runs a script as a critical task of the event loop, like AngleSharp.Js.
    /// </summary>
    sealed class LoopScriptEngine : IScriptingService
    {
        private readonly Action<ScriptOptions> _callback;

        public LoopScriptEngine(Action<ScriptOptions> callback)
        {
            _callback = callback;
        }

        public String Type => "c-sharp";

        public Boolean SupportsType(String mimeType) =>
            mimeType.Equals(Type, StringComparison.OrdinalIgnoreCase);

        public Task EvaluateScriptAsync(IResponse response, ScriptOptions options, CancellationToken cancel) =>
            options.EventLoop.EnqueueAsync(_ =>
            {
                _callback.Invoke(options);
                return true;
            }, TaskPriority.Critical);
    }
}
