namespace AngleSharp.Scripting
{
    using Jint.Native;
    using Jint.Runtime.Descriptors;
    using System;
    using System.Reflection;

    class BindingPropertyDescriptor : PropertyDescriptor
    {
        readonly PropertyInfo _property;
        readonly DomNode _host;

        public BindingPropertyDescriptor(DomNode host, PropertyInfo property)
            : base(JsValue.True, JsValue.True, false, false)
        {
            _host = host;
            _property = property;
        }

        public override JsValue? Value
        {
            get { return _property.GetValue(_host.Value).ToJsValue(_host.Engine); }
            set { _property.SetValue(_host, value.FromJsValue()); }
        }
    }
}
