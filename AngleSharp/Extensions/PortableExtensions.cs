namespace AngleSharp.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extensions for ensuring code portability.
    /// </summary>
    static class PortableExtensions
    {
        public static Task InvokeAsync(this Action action)
        {
#if !SILVERLIGHT
            return Task.Run(action);
#else
            return TaskEx.Run(action);
#endif
        }

        public static Task Delay(this CancellationToken token, Int32 timeout)
        {
#if !SILVERLIGHT
            return Task.Delay(Math.Max(timeout, 4), token);
#else
            return TaskEx.Delay(Math.Max(timeout, 4), token);
#endif
        }

        public static ConstructorInfo GetDeclaredConstructor(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().DeclaredConstructors.First();
#else
            return type.GetDeclaredConstructors().First();
#endif
        }

        public static Boolean Implements<T>(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().ImplementedInterfaces.Contains(typeof(T));
#else
            return type.GetInterfaces().Any(i => i == typeof(T));
#endif
        }

        public static ConstructorInfo[] GetDeclaredConstructors(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().DeclaredConstructors.ToArray();
#else
            return type.GetDeclaredConstructors().ToArray();
#endif
        }

        public static Type[] GetGenericTypeArguments(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().GenericTypeArguments;
#else
            return type.GetGenericTypeArguments();
#endif
        }

        public static Type[] GetGenericTypeParameters(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().GenericTypeParameters;
#else
            return type.GetGenericTypeParameters();
#endif
        }

        public static Type[] GetGenericParameterConstraints(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().GetGenericParameterConstraints();
#else
            return type.GetGenericParameterConstraints();
#endif
        }

        public static MethodInfo[] GetMethods(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().DeclaredMethods.ToArray();
#else
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
#endif
        }

        public static PropertyInfo[] GetProperties(this Type type)
        {
#if !SILVERLIGHT
            return type.GetRuntimeProperties().ToArray();
#else
            return type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
#endif
        }

        public static Type[] GetInterfaces(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().ImplementedInterfaces.ToArray();
#else
            return type.GetInterfaces();
#endif
        }

        public static ConstructorInfo[] GetConstructors(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().DeclaredConstructors.Where(c => c.IsPublic && !c.IsStatic).ToArray();
#else
            return type.GetDeclaredConstructors().Where(c => c.IsPublic && !c.IsStatic).ToArray();
#endif
        }

        public static MethodInfo GetPrivateMethod(this Type type, String name)
        {
            return GetMethod(type, name);
        }

        public static MethodInfo GetMethod(this Type type, String name)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().GetDeclaredMethod(name);
#else
            return type.GetMethod(name);
#endif
        }

        public static Boolean IsAssignableFrom(this Type type, Type fromType)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().IsAssignableFrom(fromType.GetTypeInfo());
#else
            return type.IsAssignableFrom(fromType);
#endif
        }

        public static Boolean IsDefined(this Type type, Type attributeType, Boolean inherit)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().IsDefined(attributeType, inherit);
#else
            return type.IsDefined(attributeType, inherit);
#endif
        }

        public static PropertyInfo GetProperty(this Type type, string name)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().GetDeclaredProperty(name);
#else
            return type.GetProperties().FirstOrDefault(p => p.Name == name);
#endif
        }

        public static Boolean IsStructType(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().IsValueType;
#else
            return type.IsValueType;
#endif
        }

        public static Boolean IsClassType(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().IsClass;
#else
            return type.IsClass;
#endif
        }

        public static Boolean IsAbstractClass(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().IsAbstract;
#else
            return type.IsAbstract;
#endif
        }

        public static Boolean IsNestedPrivateClass(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().IsNestedPrivate;
#else
            return type.IsNestedPrivate;
#endif
        }

        public static Boolean IsGenericClass(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().IsGenericType;
#else
            return type.IsGenericType;
#endif
        }

        public static Boolean ContainsGenericClassParameters(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().ContainsGenericParameters;
#else
            return type.ContainsGenericParameters;
#endif
        }

        public static Type GetBaseType(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().BaseType;
#else
            return type.BaseType;
#endif
        }

        public static Boolean IsGenericClassDefinition(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().IsGenericTypeDefinition;
#else
            return type.IsGenericTypeDefinition;
#endif
        }

        public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo)
        {
#if !SILVERLIGHT
            return propertyInfo.SetMethod;
#else
            return propertyInfo.GetSetMethod();
#endif
        }

        public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo)
        {
#if !SILVERLIGHT
            return propertyInfo.GetMethod;
#else
            return propertyInfo.GetGetMethod();
#endif
        }

        public static Type[] GetTypes(this Assembly assembly)
        {
#if !SILVERLIGHT
            return assembly.DefinedTypes.Select(t => t.AsType()).ToArray();
#else
            return assembly.GetTypes().ToArray();
#endif
        }

        public static Assembly GetAssembly(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().Assembly;
#else
            return type.Assembly;
#endif
        }

        public static ConstructorInfo GetConstructor(this Type type)
        {
#if !SILVERLIGHT
            return type.GetTypeInfo().DeclaredConstructors.Where(c => !c.IsStatic && c.GetParameters().Length == 0).FirstOrDefault();
#else
            return type.GetConstructors().FirstOrDefault(c => !c.IsStatic && c.GetParameters().Length == 0);
#endif
        }

        public static ConstructorInfo GetConstructor(this Type type, Type[] types)
        {
#if !SILVERLIGHT
            return GetConstructors(type).FirstOrDefault(c => c.GetParameters().Select(p => p.ParameterType).SequenceEqual(types));
#else
            return type.GetConstructors().FirstOrDefault(c => c.GetParameters().Select(p => p.ParameterType).SequenceEqual(types));
#endif
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

#if !SILVERLIGHT
        public static Boolean IsReadOnlyCollectionOfT(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>);
        }

        public static Boolean IsReadOnlyListOfT(this Type serviceType)
        {
            return serviceType.IsGenericClass() && serviceType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>);
        }
#endif
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
