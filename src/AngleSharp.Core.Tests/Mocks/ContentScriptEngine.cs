namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Network;
    using AngleSharp.Services.Scripting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    class ContentScriptEngine : IScriptEngine
    {
        readonly List<Tuple<String, ScriptOptions>> _requests;
        readonly String _type; 

        public ContentScriptEngine(String type = null)
        {
            _requests = new List<Tuple<String, ScriptOptions>>();
            _type = type ?? MimeTypeNames.DefaultJavaScript;
        }

        public String Type
        {
            get { return _type; }
        }

        public List<Tuple<String, ScriptOptions>> Requests
        {
            get { return _requests; }
        }

        public Task EvaluateScriptAsync(IResponse response, ScriptOptions options, CancellationToken cancel)
        {
            using (var sr = new StreamReader(response.Content, options.Encoding))
            {
                var source = sr.ReadToEnd();
                _requests.Add(Tuple.Create(source, options));
            }

            return Task.FromResult(false);
        }
    }
}
