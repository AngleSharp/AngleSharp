namespace AngleSharp.Scripting
{
    using Jint;
    using Jint.Native.Object;
    using System;
    using System.Reflection;

    class DomNode : ObjectInstance
    {
        readonly Object _value;

        public DomNode(Engine engine, Object value)
            : base(engine)
        {
            _value = value;
            SetProperties();
        }

        void SetProperties()
        {
            var type = _value.GetType();

            foreach (var property in type.GetProperties())
            {
                var name = property.GetCustomAttribute<DomNameAttribute>();

                if (name == null)
                    continue;

                DefineOwnProperty(name.OfficialName, new BindingPropertyDescriptor(this, property), true);
            }
        }

        public Object Value
        {
            get { return _value; }
        }
    }
}
