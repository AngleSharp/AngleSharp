namespace AngleSharp.Extensions
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// Some methods for working with bare objects.
    /// </summary>
    [DebuggerStepThrough]
    static class ObjectExtensions
    {
        /// <summary>
        /// Transforms the values of the object into a dictionary by using the
        /// provided converter.
        /// </summary>
        /// <typeparam name="T">The type of the new values.</typeparam>
        /// <param name="values">The object instance to convert.</param>
        /// <param name="converter">The supplied converter.</param>
        /// <returns>A dictionary mapping field names to values.</returns>
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

        /// <summary>
        /// Transforms the values of the object into a dictionary.
        /// </summary>
        /// <param name="values">The object instance to convert.</param>
        /// <returns>A dictionary mapping field names to values.</returns>
        public static Dictionary<String, Object> ToDictionary(this Object values)
        {
            return values.ToDictionary(m => m);
        }

        /// <summary>
        /// Tries to obtain the given key, otherwise returns the default value.
        /// </summary>
        /// <typeparam name="T">The struct type.</typeparam>
        /// <param name="values">The dictionary for the lookup.</param>
        /// <param name="key">The key to look for.</param>
        /// <returns>A nullable struct type.</returns>
        public static T? TryGet<T>(this IDictionary<String, Object> values, String key)
            where T : struct
        {
            Object value;
            
            if (values.TryGetValue(key, out value) && value is T)
                return (T)value;

            return null;
        }

        /// <summary>
        /// Tries to obtain the given key, otherwise returns null.
        /// </summary>
        /// <param name="values">The dictionary for the lookup.</param>
        /// <param name="key">The key to look for.</param>
        /// <returns>An object instance or null.</returns>
        public static Object TryGet(this IDictionary<String, Object> values, String key)
        {
            Object value;

            if (values.TryGetValue(key, out value))
                return value;

            return null;
        }

        /// <summary>
        /// Gets the value of the given key, otherwise the provided default
        /// value.
        /// </summary>
        /// <typeparam name="T">The type of the keys.</typeparam>
        /// <typeparam name="U">The type of the value.</typeparam>
        /// <param name="values">The dictionary for the lookup.</param>
        /// <param name="key">The key to look for.</param>
        /// <param name="defaultValue">The provided fallback value.</param>
        /// <returns>The value or the provided fallback.</returns>
        public static U GetOrDefault<T, U>(this IDictionary<T, U> values, T key, U defaultValue)
        {
            U value;
            return values.TryGetValue(key, out value) ? value : defaultValue;
        }

        /// <summary>
        /// Constraints the given value between the min and max values.
        /// </summary>
        /// <param name="value">The value to limit.</param>
        /// <param name="min">The lower boundary.</param>
        /// <param name="max">The upper boundary.</param>
        /// <returns>The value in the [min, max] range.</returns>
        public static Double Constraint(this Double value, Double min, Double max)
        {
            return value < min ? min : (value > max ? max : value);
        }

        /// <summary>
        /// Retrieves a string describing the error of a given error code.
        /// </summary>
        /// <param name="code">A specific error code.</param>
        /// <returns>The description of the error.</returns>
        public static String GetMessage<T>(this T code)
            where T : struct
        {
            var attr = typeof(T).GetTypeInfo().GetDeclaredField(code.ToString()).GetCustomAttribute<DomDescriptionAttribute>();

            if (attr != null)
                return attr.Description;

            return "An unknown error occurred.";
        }
    }
}
