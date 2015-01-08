namespace AngleSharp.Scripting
{
    using AngleSharp.Attributes;
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
        PropertyInfo _indexer;

        public DomNodeInstance(Engine engine, Object value)
            : base(engine)
        {
            _value = value;
            SetMembers(value.GetType());
        }

        public override PropertyDescriptor GetOwnProperty(String propertyName)
        {
            var index = 0;

            if (_indexer != null && Int32.TryParse(propertyName, out index))
                return new PropertyDescriptor(_indexer.GetMethod.Invoke(_value, new Object[] { index }).ToJsValue(Engine), false, false, false);

            return base.GetOwnProperty(propertyName);
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
                var index = property.GetCustomAttribute<DomAccessorAttribute>();

                if (index != null)
                    _indexer = property;

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
                {
                    //TODO Jint
                    // If it already has a property with the given name (usually another method),
                    // then convert that method to a two-layer method, which decides which one
                    // to pick depending on the number (and probably types) of arguments.
                    if (HasProperty(name))
                        continue;

                    FastAddProperty(name, new DomFunctionInstance(this, method), false, false, false);
                }
            }
        }

        public Object Value
        {
            get { return _value; }
        }

        public override String ToString()
        {
            return String.Format("[object {0}]", _value.GetType().Name);
        }
    }
}
