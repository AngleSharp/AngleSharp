namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Io;
    using AngleSharp.Scripting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    class ContentScriptEngine : IScriptingService
    {
        private readonly String _type; 

        public ContentScriptEngine(String type = null)
        {
            Requests = new List<Tuple<String, ScriptOptions>>();
            _type = type ?? MimeTypeNames.DefaultJavaScript;
        }

        public Boolean SupportsType(String mimeType)
        {
            return mimeType.Equals(_type, StringComparison.OrdinalIgnoreCase);
        }

        public List<Tuple<String, ScriptOptions>> Requests { get; }

        public Task EvaluateScriptAsync(IResponse response, ScriptOptions options, CancellationToken cancel)
        {
            using (var sr = new StreamReader(response.Content, options.Encoding))
            {
                var source = sr.ReadToEnd();
                Requests.Add(Tuple.Create(source, options));
            }

            return Task.FromResult(false);
        }
    }
}
