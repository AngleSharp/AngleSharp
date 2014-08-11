namespace AngleSharp.Scripting
{
    using Jint.Native;
    using Jint.Native.Function;
    using Jint.Runtime;
    using System;
    using System.Linq;
    using System.Reflection;

    sealed class DomFunctionInstance : FunctionInstance
    {
        readonly MethodInfo _method;
        readonly DomNodeInstance _host;

        public DomFunctionInstance(DomNodeInstance host, MethodInfo method)
            : base(host.Engine, GetParameters(method), null, false)
        {
            _host = host;
            _method = method;
        }

        public override JsValue Call(JsValue thisObject, JsValue[] arguments)
        {
            if (_method != null && thisObject.Type == Types.Object)
            {
                var node = thisObject.AsObject() as DomNodeInstance;

                if (node != null)
                    return _method.Invoke(node.Value, BuildArgs(arguments)).ToJsValue(Engine);
            }

            return JsValue.Undefined;
        }

        Object[] BuildArgs(JsValue[] arguments)
        {
            var parameters = _method.GetParameters();
            var max = parameters.Length;
            var args = new Object[max];

            if (max > 0 && parameters[max - 1].GetCustomAttribute<ParamArrayAttribute>() != null)
                max--;

            var n = Math.Min(arguments.Length, max);

            for (int i = 0; i < n; i++)
                args[i] = arguments[i].FromJsValue().As(parameters[i].ParameterType);

            for (int i = n; i < max; i++)
                args[i] = parameters[i].IsOptional ? parameters[i].DefaultValue : parameters[i].ParameterType.GetDefaultValue();

            if (max != parameters.Length)
            {
                var array = Array.CreateInstance(parameters[max].ParameterType.GetElementType(), Math.Max(0, arguments.Length - max));

                for (int i = max; i < arguments.Length; i++)
                    array.SetValue(arguments[i].FromJsValue(), i - max);

                args[max] = array;
            }

            return args;
        }

        static String[] GetParameters(MethodInfo method)
        {
            if (method == null)
                return new String[0];

            return method.GetParameters().Select(m => m.Name).ToArray();
        }
    }
}
