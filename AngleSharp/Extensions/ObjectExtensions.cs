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
    }
}
