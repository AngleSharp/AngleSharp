using System;
using System.Reflection;

namespace AngleSharp
{
    static class LegacyExtensions
    {
        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }

        public static Type AsType(this Type type)
        {
            return type;
        }

        public static MethodInfo GetRuntimeMethod(this Type type, String name, Type[] parameters)
        {
            return type.GetMethod(name, parameters);
        }

        public static T GetCustomAttribute<T>(this ICustomAttributeProvider provider, Boolean inherit = false) 
            where T : Attribute
        {
            var objs = provider.GetCustomAttributes(typeof(T), inherit);

            if (objs.Length > 0)
                return (T)objs[0];

            return null;
        }

        public static Delegate CreateDelegate(this MethodInfo method, Type type, Object target)
        {
            return Delegate.CreateDelegate(type, target, method);
        }
    }
}
