namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Network;
    using AngleSharp.Services.Scripting;
    using System;

    class CallbackScriptEngine : IScriptEngine
    {
        public CallbackScriptEngine(Action<ScriptOptions> callback)
        {
            Callback = callback;
        }

        public String Type
        {
            get { return "c-sharp"; }
        }

        public Action<ScriptOptions> Callback
        {
            get;
            private set;
        }

        public void Evaluate(String source, ScriptOptions options)
        {
            if (Callback != null)
                Callback(options);
        }

        public void Evaluate(IResponse response, ScriptOptions options)
        {
            if (Callback != null)
                Callback(options);
        }
    }
}
