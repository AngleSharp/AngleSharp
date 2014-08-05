namespace AngleSharp.Scripting
{
    using Jint;
using Jint.Native;
using System;

    static class Extensions
    {
        public static JsValue? ToJsValue(this Object obj, Engine engine)
        {
            if (obj == null)
                return JsValue.Null;

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

            return new DomNode(engine, obj);
        }

        public static Object FromJsValue(this JsValue? value)
        {
            if (value.HasValue)
            {
                var val = value.Value;

                switch (val.Type)
                {
                    case Jint.Runtime.Types.Boolean:
                        return val.AsBoolean();
                    case Jint.Runtime.Types.Number:
                        return val.AsNumber();
                    case Jint.Runtime.Types.String:
                        return val.AsString();
                    case Jint.Runtime.Types.Object:
                        return val.AsObject();
                }
            }

            return null;
        }
    }
}
