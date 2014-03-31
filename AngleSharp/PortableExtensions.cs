namespace AngleSharp
{
    using System;
    using System.Linq;
    using System.Reflection;

    static class PortableExtensions
    {
        public static ConstructorInfo GetDeclaredConstructor(this Type type)
        {
            return type.GetTypeInfo().DeclaredConstructors.First();
        }
    }
}
