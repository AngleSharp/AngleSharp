namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Io;
    using AngleSharp.Scripting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A scripting service whose task faults only after yielding, i.e. the failure is
    /// invisible to a caller that does not await the returned task.
    /// </summary>
    class FailingScriptEngine : IScriptingService
    {
        private readonly String _message;
        private readonly String _type;

        public FailingScriptEngine(String message, String type = null)
        {
            _message = message;
            _type = type ?? "c-sharp";
        }

        public Boolean SupportsType(String mimeType)
        {
            return mimeType.Equals(_type, StringComparison.OrdinalIgnoreCase);
        }

        public async Task EvaluateScriptAsync(IResponse response, ScriptOptions options, CancellationToken cancel)
        {
            await Task.Yield();
            throw new InvalidOperationException(_message);
        }
    }
}
