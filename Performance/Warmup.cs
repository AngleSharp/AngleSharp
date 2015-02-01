namespace AngleSharp.Performance.Html
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    sealed class Warmup : IWarmup
    {
        public void ForceJit(Type type)
        {
            ForceJit(Assembly.GetAssembly(type));
        }

        public static void ForceJit(Assembly assembly)
        {
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                var ctors = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

                foreach (var ctor in ctors)
                    JitMethod(assembly, ctor);

                var methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

                foreach (var method in methods)
                    JitMethod(assembly, method);
            }
        }

        static void JitMethod(Assembly assembly, MethodBase method)
        {
            if (method.IsAbstract || method.ContainsGenericParameters)
                return;

            RuntimeHelpers.PrepareMethod(method.MethodHandle);
        }
    }
}
