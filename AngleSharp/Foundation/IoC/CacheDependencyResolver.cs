using System;
using System.Collections.Concurrent;
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
        readonly ConcurrentDictionary<Type, Object> _cache;
        readonly ConcurrentDictionary<Type, IEnumerable<Object>> _cacheMultiple;
        readonly Func<Type, Object> _getServiceDelegate;
        readonly Func<Type, IEnumerable<Object>> _getServicesDelegate;
        readonly IDependencyResolver _resolver;

        public CacheDependencyResolver(IDependencyResolver resolver)
        {
            _cache = new ConcurrentDictionary<Type, Object>();
            _cacheMultiple = new ConcurrentDictionary<Type, IEnumerable<Object>>();
            _resolver = resolver;
            _getServiceDelegate = _resolver.GetService;
            _getServicesDelegate = _resolver.GetServices;
        }

        public Object GetService(Type serviceType)
        {
            // Use a saved delegate to prevent per-call delegate allocation
            return _cache.GetOrAdd(serviceType, _getServiceDelegate);
        }

        public IEnumerable<Object> GetServices(Type serviceType)
        {
            // Use a saved delegate to prevent per-call delegate allocation
            return _cacheMultiple.GetOrAdd(serviceType, _getServicesDelegate);
        }
    }
}
