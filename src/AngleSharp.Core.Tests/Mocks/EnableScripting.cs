namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Scripting;
    using System;

    class EnableScripting : IScriptingProvider
    {
        public IScriptEngine GetEngine(String mimeType)
        {
            return null;
        }
    }
}
