namespace AngleSharp.Scripting
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using AngleSharp.DOM.Navigator;
    using Jurassic;
    using Jurassic.Library;
    using System;

    public class Window : ObjectInstance, IWindow
    {
        public Window(ScriptEngine engine)
            : base(engine)
        {
            //this.DefineProperty("name", new PropertyDescriptor(new ), true);
        }

        public IDocument Document
        {
            get { throw new NotImplementedException(); }
        }

        public ICssStyleDeclaration GetComputedStyle(IElement element, String pseudo = null)
        {
            throw new NotImplementedException();
        }

        public String Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public INavigator Navigator
        {
            get { throw new NotImplementedException(); }
        }

        public Int32 OuterHeight
        {
            get { throw new NotImplementedException(); }
        }

        public Int32 OuterWidth
        {
            get { throw new NotImplementedException(); }
        }

        public IWindowProxy Proxy
        {
            get { throw new NotImplementedException(); }
        }

        public Int32 ScreenX
        {
            get { throw new NotImplementedException(); }
        }

        public Int32 ScreenY
        {
            get { throw new NotImplementedException(); }
        }

        public void AddEventListener(String type, EventListener callback = null, Boolean capture = false)
        {
            throw new NotImplementedException();
        }

        public Boolean Dispatch(IEvent ev)
        {
            throw new NotImplementedException();
        }

        public void RemoveEventListener(String type, EventListener callback = null, Boolean capture = false)
        {
            throw new NotImplementedException();
        }
    }
}
