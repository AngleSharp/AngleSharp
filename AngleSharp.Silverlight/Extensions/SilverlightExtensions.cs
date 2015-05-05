namespace AngleSharp.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    static class SilverlightExtensions
    {
        const Int32 UnicodePlane01Start = 0x10000;
        const Int32 UnicodePlane16End = 0x10ffff;
        const Int32 HighSurrogateStart = 0x00d800;
        const Int32 LowSurrogateEnd = 0x00dfff;

        static class CharUnicodeInfo
        {
            internal const Char HighSurrogateStart = '\ud800';
            internal const Char HighSurrogateEnd = '\udbff';
            internal const Char LowSurrogateStart = '\udc00';
            internal const Char LowSurrogateEnd = '\udfff';
        }

        public static Task InvokeAsync(this Action action)
        {
            return TaskEx.Run(action);
        }

        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }

        public static StringBuilder Insert(this StringBuilder sb, Int32 index, Char c)
        {
            return sb.Insert(index, c.ToString());
        }

        public static FieldInfo GetDeclaredField(this Type type, String name)
        {
            return type.GetField(name);
        }

        public static T GetCustomAttribute<T>(this ICustomAttributeProvider provider, Boolean inherit = false)
            where T : Attribute
        {
            var objs = provider.GetCustomAttributes(typeof(T), inherit);

            if (objs.Length > 0)
                return (T)objs[0];

            return null;
        }

        public static String ConvertFromUtf32(this Int32 utf32)
        {
            // For UTF32 values from U+00D800 ~ U+00DFFF, we should throw.  They
            // are considered as irregular code unit sequence, but they are not illegal.
            if ((utf32 < 0 || utf32 > UnicodePlane16End) || (utf32 >= HighSurrogateStart && utf32 <= LowSurrogateEnd))
                throw new ArgumentOutOfRangeException("utf32", "ArgumentOutOfRange_InvalidUTF32");

            if (utf32 < UnicodePlane01Start)
                return Char.ToString((Char)utf32);

            // This is a sumplementary character.  Convert it to a surrogate pair in UTF-16.
            utf32 -= UnicodePlane01Start;
            var surrogate = new Char[2];
            surrogate[0] = (Char)((utf32 / 0x400) + (Int32)CharUnicodeInfo.HighSurrogateStart);
            surrogate[1] = (Char)((utf32 % 0x400) + (Int32)CharUnicodeInfo.LowSurrogateStart);
            return new String(surrogate);
        }

        public static Int32 ConvertToUtf32(this String s, Int32 index)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            if (index < 0 || index >= s.Length)
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange_Index");

            var temp1 = (Int32)s[index] - CharUnicodeInfo.HighSurrogateStart;

            if (temp1 >= 0 && temp1 <= 0x7ff)
            {
                if (temp1 <= 0x3ff)
                {
                    if (index < s.Length - 1)
                    {
                        var temp2 = (Int32)s[index + 1] - CharUnicodeInfo.LowSurrogateStart;

                        if (temp2 >= 0 && temp2 <= 0x3ff)
                            return (temp1 * 0x400) + temp2 + UnicodePlane01Start;

                        throw new ArgumentException("Argument_InvalidHighSurrogate", "s");
                    }

                    throw new ArgumentException("Argument_InvalidHighSurrogate", "s");
                }

                throw new ArgumentException("Argument_InvalidLowSurrogate", "s");
            }

            return (Int32)s[index];
        }

        public static Task Delay(this CancellationToken token, Int32 timeout)
        {
            return TaskEx.Delay(Math.Max(timeout, 4), token);
        }

        public static ConstructorInfo GetDeclaredConstructor(this Type type)
        {
            return type.GetDeclaredConstructors().First();
        }

        public static Boolean Implements<T>(this Type type)
        {
            return type.GetInterfaces().Any(i => i == typeof(T));
        }

        public static ConstructorInfo[] GetDeclaredConstructors(this Type type)
        {
            return type.GetDeclaredConstructors().ToArray();
        }

        public static Type[] GetGenericTypeArguments(this Type type)
        {
            return type.GetGenericTypeArguments();
        }

        public static Type[] GetGenericTypeParameters(this Type type)
        {
            return type.GetGenericTypeParameters();
        }

        public static Type[] GetGenericParameterConstraints(this Type type)
        {
            return type.GetGenericParameterConstraints();
        }

        public static MethodInfo[] GetMethods(this Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        }

        public static PropertyInfo[] GetProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
        }

        public static Type[] GetInterfaces(this Type type)
        {
            return type.GetInterfaces();
        }

        public static ConstructorInfo[] GetConstructors(this Type type)
        {
            return type.GetDeclaredConstructors().Where(c => c.IsPublic && !c.IsStatic).ToArray();
        }

        public static MethodInfo GetPrivateMethod(this Type type, String name)
        {
            return GetMethod(type, name);
        }

        public static MethodInfo GetMethod(this Type type, String name)
        {
            return type.GetMethod(name);
        }

        public static Boolean IsAssignableFrom(this Type type, Type fromType)
        {
            return type.IsAssignableFrom(fromType);
        }

        public static Boolean IsDefined(this Type type, Type attributeType, Boolean inherit)
        {
            return type.IsDefined(attributeType, inherit);
        }

        public static PropertyInfo GetProperty(this Type type, string name)
        {
            return type.GetProperties().FirstOrDefault(p => p.Name == name);
        }

        public static Boolean IsStructType(this Type type)
        {
            return type.IsValueType;
        }

        public static Boolean IsClassType(this Type type)
        {
            return type.IsClass;
        }

        public static Boolean IsAbstractClass(this Type type)
        {
            return type.IsAbstract;
        }

        public static Boolean IsNestedPrivateClass(this Type type)
        {
            return type.IsNestedPrivate;
        }

        public static Boolean IsGenericClass(this Type type)
        {
            return type.IsGenericType;
        }

        public static Boolean ContainsGenericClassParameters(this Type type)
        {
            return type.ContainsGenericParameters;
        }

        public static Type GetBaseType(this Type type)
        {
            return type.BaseType;
        }

        public static Boolean IsGenericClassDefinition(this Type type)
        {
            return type.IsGenericTypeDefinition;
        }

        public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetSetMethod();
        }

        public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetGetMethod();
        }

        public static Type[] GetTypes(this Assembly assembly)
        {
            return assembly.GetTypes().ToArray();
        }

        public static Assembly GetAssembly(this Type type)
        {
            return type.Assembly;
        }

        public static ConstructorInfo GetConstructor(this Type type)
        {
            return type.GetConstructors().FirstOrDefault(c => !c.IsStatic && c.GetParameters().Length == 0);
        }

        public static ConstructorInfo GetConstructor(this Type type, Type[] types)
        {
            return type.GetConstructors().FirstOrDefault(c => c.GetParameters().Select(p => p.ParameterType).SequenceEqual(types));
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
