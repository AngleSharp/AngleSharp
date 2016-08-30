#if !NET40 && !SL50
namespace AngleSharp.Extensions
{
    using System;
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

        public static Task Delay(this CancellationToken token, Int32 timeout)
        {
            return Task.Delay(Math.Max(timeout, 4), token);
        }

        public static Boolean Implements<T>(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces.Contains(typeof(T));
        }

        public static PropertyInfo[] GetProperties(this Type type)
        {
            return type.GetRuntimeProperties().ToArray();
        }

        public static ConstructorInfo[] GetConstructors(this Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors.Where(c => c.IsPublic && !c.IsStatic).ToArray();
        }

        public static FieldInfo GetField(this Type type, String name)
        {
            return type.GetTypeInfo().GetDeclaredField(name);
        }

        public static PropertyInfo GetProperty(this Type type, String name)
        {
            return type.GetTypeInfo().GetDeclaredProperty(name);
        }

        public static Boolean IsAbstractClass(this Type type)
        {
            return type.GetTypeInfo().IsAbstract;
        }

        public static Type[] GetTypes(this Assembly assembly)
        {
            return assembly.DefinedTypes.Select(t => t.AsType()).ToArray();
        }

        public static Assembly GetAssembly(this Type type)
        {
            return type.GetTypeInfo().Assembly;
        }
    }
}
#endif