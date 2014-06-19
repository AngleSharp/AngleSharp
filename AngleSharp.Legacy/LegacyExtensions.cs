namespace AngleSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

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

        public static ConstructorInfo GetDeclaredConstructor(this Type type)
        {
            return type.GetConstructors().First();
        }

        public static ConstructorInfo[] GetDeclaredConstructors(this Type type)
        {
            return type.GetConstructors().ToArray();
        }

        public static FieldInfo GetDeclaredField(this Type type, String name)
        {
            return type.GetField(name);
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

        public static Type[] GetGenericTypeArguments(this Type type)
        {
            return type.GetGenericArguments();
        }

        public static Boolean IsStructType(this Type type)
        {
            return type.IsValueType;
        }

        public static Boolean IsClassType(this Type type)
        {
            return type.IsClass;
        }

        public static Boolean IsGenericClass(this Type type)
        {
            return type.IsGenericType;
        }

        public static MethodInfo GetMethodInfo(this Delegate method)
        {
            return method.Method;
        }

        public static Boolean IsLazy(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(Lazy<>);
        }

        public static MethodInfo GetPrivateMethod(this Type type, String name)
        {
            return type.GetMethod(name);
        }

        public static Assembly GetAssembly(this Type type)
        {
            return type.Assembly;
        }

        public static Boolean IsFunc(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(Func<>);
        }

        public static Boolean IsFuncWithParameters(this Type serviceType)
        {
            if (!serviceType.IsGenericClass())
                return false;

            var genericTypeDefinition = serviceType.GetGenericTypeDefinition();
            return genericTypeDefinition == typeof(Func<,>) || genericTypeDefinition == typeof(Func<,,>) || genericTypeDefinition == typeof(Func<,,,>) || genericTypeDefinition == typeof(Func<,,,,>);
        }

        public static Boolean IsListOfT(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(IList<>);
        }

        public static Boolean IsEnumerableOfT(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        public static Boolean IsClosedGeneric(this Type serviceType)
        {
            return serviceType.IsGenericClass() && !serviceType.IsGenericTypeDefinition;
        }

        public static Boolean IsAbstractClass(this Type type)
        {
            return type.IsAbstract;
        }

        public static Boolean IsNestedPrivateClass(this Type type)
        {
            return type.IsNestedPrivate;
        }

        public static Boolean IsCollectionOfT(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(ICollection<>);
        }

        public static Boolean ContainsGenericClassParameters(this Type type)
        {
            return type.ContainsGenericParameters;
        }

        public static Boolean IsGenericClassDefinition(this Type type)
        {
            return type.IsGenericTypeDefinition;
        }

        public static Type GetBaseType(this Type type)
        {
            return type.BaseType;
        }

        public static Object[] GetCustomAttributes(this Assembly assembly, Type attributeType)
        {
            return assembly.GetCustomAttributes(attributeType, false);
        }
    }
}
