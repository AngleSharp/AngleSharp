namespace AngleSharp.Scripting
{
    using AngleSharp.Infrastructure;
    using Jint;
    using System;
    using System.IO;
    using System.Text;

    public class JavaScriptEngine : IScriptEngine
    {
        readonly Engine _engine;
        readonly Window _window;

        public JavaScriptEngine()
        {
            _engine = new Engine();
        }

        public String Type
        {
            get { return "text/javascript"; }
        }

        public void Evaluate(String source, ScriptOptions options)
        {
            SetOptions(options);
            _engine.EnterExecutionContext(_engine.GlobalEnvironment, _engine.GlobalEnvironment, _window);
            _engine.Execute(source);
            _engine.LeaveExecutionContext();

        }

        public void Evaluate(Stream source, ScriptOptions options)
        {
            var reader = new StreamReader(source, options.Encoding ?? Encoding.UTF8, true);
            var content = reader.ReadToEnd();
            reader.Close();
            Evaluate(content, options);
        }

        void SetOptions(ScriptOptions options)
        {
            _window.Document = options.Document;
            _window.Window = options.Context;
        }
    }
}
