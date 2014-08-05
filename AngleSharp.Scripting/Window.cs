namespace AngleSharp.Scripting
{
    using AngleSharp.DOM;
    using Jint;
    using Jint.Native.Object;
    using Jint.Runtime.Descriptors;

    public class Window : ObjectInstance
    {
        public Window(Engine engine)
            : base(engine)
        {
            this.DefineOwnProperty("name", new PropertyDescriptor(), true);
            //this.DefineProperty("name", new PropertyDescriptor(new ), true);
        }

        public IWindow Window
        {
            get;
            set;
        }

        public IDocument Document
        {
            get;
            set;
        }
    }
}
