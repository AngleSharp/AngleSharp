namespace AngleSharp.Scripting
{
    using Jint;
    using Jint.Native.Object;
    using System;
    using System.Linq;
    using System.Reflection;

    class DomNode : ObjectInstance
    {
        readonly Object _value;

        public DomNode(Engine engine, Object value)
            : base(engine)
        {
            _value = value;
            SetMembers(value.GetType());
        }

        void SetMembers(Type type)
        {
            if (type.GetCustomAttribute<DomNameAttribute>() == null)
            {
                foreach (var contract in type.GetInterfaces())
                    SetMembers(contract);
            }
            else
            {
                SetProperties(type.GetProperties());
                SetMethods(type.GetMethods());
            }
        }

        void SetProperties(PropertyInfo[] properties)
        {
            foreach (var property in properties)
            {
                var names = property.GetCustomAttributes<DomNameAttribute>();

                foreach (var name in names.Select(m => m.OfficialName))
                    FastSetProperty(name, new BindingPropertyDescriptor(this, property));
            }
        }

        void SetMethods(MethodInfo[] methods)
        {
            foreach (var method in methods)
            {
                var names = method.GetCustomAttributes<DomNameAttribute>();

                foreach (var name in names.Select(m => m.OfficialName))
                {
                    //new BindFunctionInstance(Engine)
                }
            }
        }

        public Object Value
        {
            get { return _value; }
        }
    }
}
