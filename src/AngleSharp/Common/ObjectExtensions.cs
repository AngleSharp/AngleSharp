namespace AngleSharp.Common
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Some methods for working with bare objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Transforms the values of the object into a dictionary of strings.
        /// </summary>
        /// <param name="values">The object instance to convert.</param>
        /// <returns>A dictionary mapping field names to values.</returns>
        public static Dictionary<String, String> ToDictionary(this Object values)
        {
            var symbols = new Dictionary<String, String>();

            if (values != null)
            {
                var properties = values.GetType().GetProperties();

                foreach (var property in properties)
                {
                    var value = property.GetValue(values, null) ?? String.Empty;
                    symbols.Add(property.Name, value.ToString());
                }
            }

            return symbols;
        }

        /// <summary>
        /// Gets an item from the enumerable by its index. Throws an exception
        /// if the provided index is invalid.
        /// </summary>
        /// <typeparam name="T">The type of enumerable.</typeparam>
        /// <param name="items">The items to iterate over.</param>
        /// <param name="index">The index to look for.</param>
        /// <returns>The item at the specified index.</returns>
        public static T GetItemByIndex<T>(this IEnumerable<T> items, Int32 index)
        {
            if (index >= 0)
            {
                var i = 0;

                foreach (var item in items)
                {
                    if (i++ == index)
                    {
                        return item;
                    }
                }
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        /// <summary>
        /// Returns the concatenation of the provided enumerable with the
        /// specified element. The item is added to the beginning.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="element">The item to concat.</param>
        /// <returns>The new enumerable.</returns>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, T element)
        {
            yield return element;

            foreach (var item in items)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Returns the removal of the specified element from the provided
        /// enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="element">The item to remove.</param>
        /// <returns>The new enumerable.</returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> items, T element)
        {
            foreach (var item in items)
            {
                if (!Object.ReferenceEquals(item, element))
                {
                    yield return item;
                }
            }
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
            if (values.TryGetValue(key, out var value) && value is T)
            {
                return (T)value;
            }

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
            values.TryGetValue(key, out var value);
            return value;
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
            return values.TryGetValue(key, out var value) ? value : defaultValue;
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
            var field = typeof(T).GetField(code.ToString());
            var description = field.GetCustomAttribute<DomDescriptionAttribute>()?.Description;
            return description ?? "An unknown error occurred.";
        }
    }
}
