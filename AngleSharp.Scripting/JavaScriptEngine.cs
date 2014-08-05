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
            var context = new DomNode(_engine, options.Context);
            _engine.EnterExecutionContext(_engine.GlobalEnvironment, _engine.GlobalEnvironment, context);
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
    }
}
