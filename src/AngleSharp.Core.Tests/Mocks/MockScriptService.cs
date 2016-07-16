namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Services;
    using AngleSharp.Services.Scripting;
    using System;

    class MockScriptService<T> : IScriptingProvider
        where T : IScriptEngine
    {
        readonly T _engine;

        public MockScriptService(T engine)
        {
            _engine = engine;
        }

        public IScriptEngine GetEngine(String mimeType)
        {
            return _engine;
        }
    }
}
