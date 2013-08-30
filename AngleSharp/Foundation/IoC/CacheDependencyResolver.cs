using AngleSharp.Interfaces;
using System;
using System.Collections.Generic;

namespace AngleSharp
{
    /// <summary>
    /// Wraps an IDependencyResolver and ensures single instance per-type.
    /// </summary>
    /// <remarks>
    /// Note it's possible for multiple threads to race and call the _resolver service multiple times.
    /// We'll pick one winner and ignore the others and still guarantee a unique instance.
    /// </remarks>
    sealed class CacheDependencyResolver : IDependencyResolver
    {
        readonly Dictionary<Type, Object> _cache;
        readonly Dictionary<Type, IEnumerable<Object>> _cacheMultiple;
        readonly Func<Type, Object> _getServiceDelegate;
        readonly Func<Type, IEnumerable<Object>> _getServicesDelegate;
        readonly IDependencyResolver _resolver;

        static Object mylock;

        static CacheDependencyResolver()
        {
            mylock = new Object();
        }

        public CacheDependencyResolver(IDependencyResolver resolver)
        {
            _cache = new Dictionary<Type, Object>();
            _cacheMultiple = new Dictionary<Type, IEnumerable<Object>>();
            _resolver = resolver;
            _getServiceDelegate = _resolver.GetService;
            _getServicesDelegate = _resolver.GetServices;
        }

        static T GetOrAdd<T>(Dictionary<Type, T> elements, Type serviceType, Func<Type, T> serviceDelegate)
        {
            lock (mylock)
            {
                if (elements.ContainsKey(serviceType))
                    return elements[serviceType];

                var serviceValue = serviceDelegate(serviceType);
                elements.Add(serviceType, serviceValue);
                return serviceValue;
            }
        }

        public Object GetService(Type serviceType)
        {
            // Use a saved delegate to prevent per-call delegate allocation
            return GetOrAdd(_cache, serviceType, _getServiceDelegate);
        }

        public IEnumerable<Object> GetServices(Type serviceType)
        {
            // Use a saved delegate to prevent per-call delegate allocation
            return GetOrAdd(_cacheMultiple, serviceType, _getServicesDelegate);
        }
    }
}
