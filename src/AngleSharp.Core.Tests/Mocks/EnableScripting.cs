namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Scripting;
    using AngleSharp.Scripting.Services;
    using System;

    class EnableScripting : IScriptingProvider
    {
        public IScriptEngine GetEngine(String mimeType)
        {
            return null;
        }
    }
}
