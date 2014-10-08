namespace AngleSharp.Scripting
{
    using Jint;
    using Jint.Native.Object;
    using Jint.Runtime.Descriptors;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    sealed class DomNodeInstance : ObjectInstance
    {
        readonly Object _value;

        public DomNodeInstance(Engine engine, Object value)
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
                SetEvents(type.GetEvents());
            }
        }

        void SetEvents(EventInfo[] eventInfos)
        {
            foreach (var eventInfo in eventInfos)
            {
                var names = eventInfo.GetCustomAttributes<DomNameAttribute>();

                foreach (var name in names.Select(m => m.OfficialName))
                {
                    FastSetProperty(name, new PropertyDescriptor(
                        new DomFunctionInstance(this, eventInfo.RaiseMethod),
                        new DomFunctionInstance(this, eventInfo.AddMethod), false, false));
                }
            }
        }

        void SetProperties(IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                var names = property.GetCustomAttributes<DomNameAttribute>();

                foreach (var name in names.Select(m => m.OfficialName))
                {
                    FastSetProperty(name, new PropertyDescriptor(
                        new DomFunctionInstance(this, property.GetMethod),
                        new DomFunctionInstance(this, property.SetMethod), false, false));
                }
            }
        }

        void SetMethods(IEnumerable<MethodInfo> methods)
        {
            foreach (var method in methods)
            {
                var names = method.GetCustomAttributes<DomNameAttribute>();

                foreach (var name in names.Select(m => m.OfficialName))
                    FastAddProperty(name, new DomFunctionInstance(this, method), false, false, false);
            }
        }

        public Object Value
        {
            get { return _value; }
        }
    }
}
