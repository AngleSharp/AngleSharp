namespace AngleSharp
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extensions for ensuring code portability.
    /// </summary>
    static class PortableExtensions
    {
        public static Boolean Implements<T>(this Type type) => type.GetTypeInfo().ImplementedInterfaces.Contains(typeof(T));
    }
}
