namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Scripting;
    using AngleSharp.Services;
    using System;

    class EnableScripting : IScriptingService
    {
        public IScriptEngine GetEngine(String mimeType)
        {
            return null;
        }
    }
}
