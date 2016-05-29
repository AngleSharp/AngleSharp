namespace AngleSharp.Core.Tests.Mocks
{
    using System;
    using AngleSharp.Services;
    using AngleSharp.Services.Scripting;

    class EnableScripting : IScriptingProvider
    {
        public IScriptEngine GetEngine(String mimeType)
        {
            return null;
        }
    }
}
