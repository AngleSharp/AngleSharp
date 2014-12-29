namespace AngleSharp.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Some methods for working with bare objects.
    /// </summary>
    [DebuggerStepThrough]
    static class ObjectExtensions
    {
        public static Dictionary<String, T> ToDictionary<T>(this Object values, Func<Object, T> converter)
        {
            var symbols = new Dictionary<String, T>();

            if (values != null)
            {
                var properties = values.GetType().GetProperties();

                foreach (var property in properties)
                {
                    var value = property.GetValue(values, null) ?? String.Empty;
                    symbols.Add(property.Name, converter(value));
                }
            }

            return symbols;
        }

        public static Dictionary<String, Object> ToDictionary(this Object values)
        {
            return values.ToDictionary(m => m);
        }

        public static T? TryCast<T>(this Object value)
            where T : struct
        {
            if (value is T)
                return (T)value;

            return null;
        }
    }
}
