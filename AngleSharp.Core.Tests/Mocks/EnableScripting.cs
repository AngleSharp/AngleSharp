namespace AngleSharp.Core.Tests.Mocks
{
    using System;
    using AngleSharp.Services;
    using AngleSharp.Services.Scripting;

    class EnableScripting : IScriptingService
    {
        public IScriptEngine GetEngine(String mimeType)
        {
            return null;
        }
    }
}
