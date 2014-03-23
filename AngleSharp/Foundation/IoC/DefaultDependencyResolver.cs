namespace AngleSharp
{
    using AngleSharp.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    sealed class DefaultDependencyResolver : IDependencyResolver
    {
        readonly Dictionary<Type, Func<Object>> _mapping;

        public DefaultDependencyResolver()
        {
            _mapping = new Dictionary<Type, Func<Object>>();
        }

        public Object GetService(Type serviceType)
        {
            var serviceTypeInfo = serviceType.GetTypeInfo();

            if (serviceTypeInfo.IsInterface || serviceTypeInfo.IsAbstract)
                return GetServiceFromMapping(serviceTypeInfo.AsType());

            try { return Activator.CreateInstance(serviceType); }
            catch { return null; }
        }

        Object GetServiceFromMapping(Type type)
        {
            Func<Object> creator = null;

            if (_mapping.TryGetValue(type, out creator))
                return creator();

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

        public void AddService<TInterface, TTarget>(Func<TTarget> target)
            where TTarget : TInterface
        {
            _mapping[typeof(TInterface)] = () => target();
        }
    }
}
