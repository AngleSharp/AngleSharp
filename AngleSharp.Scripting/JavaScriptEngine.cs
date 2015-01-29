namespace AngleSharp.Scripting
{
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using Jint;
    using Jint.Runtime.Environments;
    using System;
    using System.IO;
    using System.Text;

    public class JavaScriptEngine : IScriptEngine
    {
        readonly Engine _engine;
        readonly LexicalEnvironment _variable;

        public JavaScriptEngine()
        {
            _engine = new Engine();
            _engine.SetValue("console", new ConsoleInstance(_engine));
            _variable = LexicalEnvironment.NewObjectEnvironment(_engine, _engine.Global, null, false);
        }

        public String Type
        {
            get { return "text/javascript"; }
        }

        public Object Result
        {
            get { return _engine.GetCompletionValue(); }
        }

        public void Evaluate(String source, ScriptOptions options)
        {
            var context = new DomNodeInstance(_engine, options.Context);
            var env = LexicalEnvironment.NewObjectEnvironment(_engine, context, _engine.ExecutionContext.LexicalEnvironment, true);
            _engine.EnterExecutionContext(env, _variable, context);
            _engine.Execute(source);
            _engine.LeaveExecutionContext();
        }

        public void Evaluate(IResponse response, ScriptOptions options)
        {
            var reader = new StreamReader(response.Content, options.Encoding ?? Encoding.UTF8, true);
            var content = reader.ReadToEnd();
            reader.Close();
            Evaluate(content, options);
        }

        public void Reset()
        {
            //TODO Jint
        }
    }
}
