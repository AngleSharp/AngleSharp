namespace AngleSharp.Scripting
{
    using Jint;
    using Jint.Native.Object;
    using Jint.Runtime.Interop;
    using System;

    sealed class ConsoleInstance : ObjectInstance
    {
        public ConsoleInstance(Engine engine)
            : base(engine)
        {
            Action<Object> log = obj => Console.WriteLine(obj);
            FastAddProperty("log", new DelegateWrapper(engine, log), false, false, false);
        }
    }
}
