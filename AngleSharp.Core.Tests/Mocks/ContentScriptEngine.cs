namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Network;
    using AngleSharp.Services.Scripting;
    using System;
    using System.Collections.Generic;
    using System.IO;

    class ContentScriptEngine : IScriptEngine
    {
        readonly List<Tuple<String, ScriptOptions>> _requests;
        readonly String _type; 

        public ContentScriptEngine(String type = null)
        {
            _requests = new List<Tuple<String, ScriptOptions>>();
            _type = type ?? MimeTypes.DefaultJavaScript;
        }

        public String Type
        {
            get {return _type;}
        }

        public List<Tuple<String, ScriptOptions>> Requests
        {
            get { return _requests; }
        }

        public void Evaluate(String source, ScriptOptions options)
        {
            _requests.Add(Tuple.Create(source, options));
        }

        public void Evaluate(IResponse response, ScriptOptions options)
        {
            using (var sr = new StreamReader(response.Content, options.Encoding))
            {
                var source = sr.ReadToEnd();
                Evaluate(source, options);
            }
        }
    }
}
