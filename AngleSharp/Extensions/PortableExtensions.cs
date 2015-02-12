namespace AngleSharp.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extensions for ensuring code portability.
    /// </summary>
    static class PortableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ConvertFromUtf32(this Int32 utf32)
        {
            return Char.ConvertFromUtf32(utf32);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ConvertToUtf32(this String s, Int32 index)
        {
            return Char.ConvertToUtf32(s, index);
        }

        public static Task InvokeAsync(this Action action)
        {
            return Task.Run(action);
        }

        public static Task Delay(this CancellationToken token, Int32 timeout)
        {
            return Task.Delay(Math.Max(timeout, 4), token);
        }

        public static ConstructorInfo GetDeclaredConstructor(this Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors.First();
        }

        public static Boolean Implements<T>(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces.Contains(typeof(T));
        }

        public static ConstructorInfo[] GetDeclaredConstructors(this Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors.ToArray();
        }

        public static Type[] GetGenericTypeArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeArguments;
        }

        public static Type[] GetGenericTypeParameters(this Type type)
        {
            return type.GetTypeInfo().GenericTypeParameters;
        }

        public static Type[] GetGenericParameterConstraints(this Type type)
        {
            return type.GetTypeInfo().GetGenericParameterConstraints();
        }

        public static MethodInfo[] GetMethods(this Type type)
        {
            return type.GetTypeInfo().DeclaredMethods.ToArray();
        }

        public static PropertyInfo[] GetProperties(this Type type)
        {
            return type.GetRuntimeProperties().ToArray();
        }

        public static Type[] GetInterfaces(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces.ToArray();
        }

        public static ConstructorInfo[] GetConstructors(this Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors.Where(c => c.IsPublic && !c.IsStatic).ToArray();
        }

        public static MethodInfo GetPrivateMethod(this Type type, String name)
        {
            return GetMethod(type, name);
        }

        public static MethodInfo GetMethod(this Type type, String name)
        {
            return type.GetTypeInfo().GetDeclaredMethod(name);
        }

        public static Boolean IsAssignableFrom(this Type type, Type fromType)
        {
            return type.GetTypeInfo().IsAssignableFrom(fromType.GetTypeInfo());
        }

        public static Boolean IsDefined(this Type type, Type attributeType, Boolean inherit)
        {
            return type.GetTypeInfo().IsDefined(attributeType, inherit);
        }

        public static PropertyInfo GetProperty(this Type type, string name)
        {
            return type.GetTypeInfo().GetDeclaredProperty(name);
        }

        public static Boolean IsStructType(this Type type)
        {
            return type.GetTypeInfo().IsValueType;
        }

        public static Boolean IsClassType(this Type type)
        {
            return type.GetTypeInfo().IsClass;
        }

        public static Boolean IsAbstractClass(this Type type)
        {
            return type.GetTypeInfo().IsAbstract;
        }

        public static Boolean IsNestedPrivateClass(this Type type)
        {
            return type.GetTypeInfo().IsNestedPrivate;
        }

        public static Boolean IsGenericClass(this Type type)
        {
            return type.GetTypeInfo().IsGenericType;
        }

        public static Boolean ContainsGenericClassParameters(this Type type)
        {
            return type.GetTypeInfo().ContainsGenericParameters;
        }

        public static Type GetBaseType(this Type type)
        {
            return type.GetTypeInfo().BaseType;
        }

        public static Boolean IsGenericClassDefinition(this Type type)
        {
            return type.GetTypeInfo().IsGenericTypeDefinition;
        }

        public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo)
        {
            return propertyInfo.SetMethod;
        }

        public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetMethod;
        }

        public static Type[] GetTypes(this Assembly assembly)
        {
            return assembly.DefinedTypes.Select(t => t.AsType()).ToArray();
        }

        public static Assembly GetAssembly(this Type type)
        {
            return type.GetTypeInfo().Assembly;
        }

        public static ConstructorInfo GetConstructor(this Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors.Where(c => !c.IsStatic && c.GetParameters().Length == 0).FirstOrDefault();
        }

        public static ConstructorInfo GetConstructor(this Type type, Type[] types)
        {
            return GetConstructors(type).FirstOrDefault(c => c.GetParameters().Select(p => p.ParameterType).SequenceEqual(types));
        }

        public static Boolean IsEnumerableOfT(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        public static Boolean IsListOfT(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(IList<>);
        }

        public static Boolean IsCollectionOfT(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(ICollection<>);
        }

        public static Boolean IsReadOnlyCollectionOfT(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>);
        }

        public static Boolean IsReadOnlyListOfT(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>);
        }

        public static Boolean IsLazy(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(Lazy<>);
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

        public static Boolean IsClosedGeneric(this Type serviceType)
        {
            return serviceType.IsGenericClass() && !serviceType.IsGenericClassDefinition();
        }

        public static Boolean IsGenericGetInstanceMethod(this MethodInfo m)
        {
            return m.Name == "GetInstance" && m.IsGenericMethodDefinition;
        }

        public static Type GetElementType(this Type type)
        {
            if (type.IsGenericClass() && type.GetGenericTypeArguments().Count() == 1)
            {
                return type.GetGenericTypeArguments()[0];
            }

            return type.GetElementType();
        }
    }
}
