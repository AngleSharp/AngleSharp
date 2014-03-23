namespace AngleSharp
{
    using AngleSharp.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    sealed class DefaultDependencyResolver : IDependencyResolver
    {
        readonly Dictionary<Type, Type> _mapping;

        public DefaultDependencyResolver()
        {
            _mapping = new Dictionary<Type, Type>();
        }

        public Object GetService(Type serviceType)
        {
            var serviceTypeInfo = serviceType.GetTypeInfo();

            if (serviceTypeInfo.IsInterface || serviceTypeInfo.IsAbstract)
                return GetServiceFromMapping(serviceTypeInfo.AsType());

            try { return Activator.CreateInstance(serviceType); }
            catch { return null; }
        }

        Object GetServiceFromMapping(Type interfaceType)
        {
            //foreach (var type in typeInfo.Assembly.DefinedTypes.Where(m => !m.IsInterface && !m.IsAbstract))
            //{
            //    if (type.IsSubclassOf(type.AsType()))
            //    {

            //    }
            //}

            if (_mapping.ContainsKey(interfaceType))
                return GetService(_mapping[interfaceType]);

            return null;
        }

        public IEnumerable<Object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<Object>();
        }

        public void RemoveService<TInterface>()
        {
            var key = typeof(TInterface);

            if (_mapping.ContainsKey(key))
                _mapping.Remove(key);
        }

        public void AddService<TInterface, TTarget>()
        {
            _mapping[typeof(TInterface)] = typeof(TTarget);
        }
    }
}
