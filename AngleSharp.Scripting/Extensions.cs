namespace AngleSharp.Scripting
{
    using Jint;
    using Jint.Native;
    using Jint.Native.Function;
    using Jint.Runtime;
    using System;
    using System.Linq.Expressions;

    static class Extensions
    {
        public static JsValue ToJsValue(this Object obj, Engine engine)
        {
            if (obj == null)
                return JsValue.Undefined;

            if (obj is String)
                return new JsValue((String)obj);
            else if (obj is Int32)
                return new JsValue((Int32)obj);
            else if (obj is UInt32)
                return new JsValue((UInt32)obj);
            else if (obj is Double)
                return new JsValue((Double)obj);
            else if (obj is Single)
                return new JsValue((Single)obj);
            else if (obj is Boolean)
                return new JsValue((Boolean)obj);

            return new DomNodeInstance(engine, obj);
        }

        public static Object FromJsValue(this JsValue val)
        {
            switch (val.Type)
            {
                case Types.Boolean:
                    return val.AsBoolean();
                case Types.Number:
                    return val.AsNumber();
                case Types.String:
                    return val.AsString();
                case Types.Object:
                    var obj = val.AsObject();
                    var node = obj as DomNodeInstance;

                    if (node != null)
                        return node.Value;

                    return obj;
                case Types.Undefined:
                case Types.Null:
                    return null;
            }

            return val.ToObject();
        }

        public static Object As(this Object value, Type targetType)
        {
            if (value == null)
                return value;

            var sourceType = value.GetType();

            if (sourceType == targetType || sourceType.IsSubclassOf(targetType) || targetType.IsInstanceOfType(value) || targetType.IsAssignableFrom(sourceType))
                return value;

            if (targetType.IsSubclassOf(typeof(Delegate)) && value is FunctionInstance)
                return targetType.ToDelegate((FunctionInstance)value);

            if (sourceType.CanConvert(targetType))
                return Expression.Convert(Expression.Parameter(sourceType, null), targetType).Method.Invoke(value, null);

            throw new JavaScriptException("The provided parameter is invalid.");
        }

        public static Object GetDefaultValue(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static Boolean CanConvert(this Type fromType, Type toType)
        {
            try
            {
                // Throws an exception if there is no conversion from fromType to toType
                Expression.Convert(Expression.Parameter(fromType, null), toType);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
