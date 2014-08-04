namespace AngleSharp.Scripting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class Interfaces
    {
        readonly Type[] _types;

        public Interfaces(Assembly source)
        {
            _types = source.ExportedTypes.ToArray();
        }

        public IEnumerable<Type> GetClasses
        {
            get 
            { 
                var baseTypes = _types.Where(m => m.IsInterface && m.GetCustomAttribute<DomNameAttribute>() != null);
                yield break;
            }
        }

        public IEnumerable<Type> GetEnums
        {
            get
            {
                var baseTypes = _types.Where(m => m.IsEnum && m.GetCustomAttribute<DomNameAttribute>() != null);
                yield break;
            }
        }
    }
}
