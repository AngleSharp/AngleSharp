namespace AngleSharp.Scripting
{
    using AngleSharp.DOM;
    using Jurassic;
    using Jurassic.Library;

    class DomNode<T> : ObjectInstance
        where T : INode
    {
        readonly T _instance;

        public DomNode(ScriptEngine engine, T instance)
            : base(engine)
        {
        }
    }
}
