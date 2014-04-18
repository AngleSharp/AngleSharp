namespace AngleSharp
{
    using AngleSharp.Network;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Represents the dependency resolver for supplying
    /// inversion of control.
    /// </summary>
    public class DependencyResolver
    {
        #region Fields

        IDependencyResolver _current;
        CacheDependencyResolver _currentCache;

        #endregion

        #region Singleton

        static readonly DependencyResolver _instance = new DependencyResolver();

        /// <summary>
        /// Gets the current resolver.
        /// </summary>
        public static IDependencyResolver Current
        {
            get { return _instance._current; }
        }

        #endregion

        #region ctor

        /// <summary>
        /// Creates an instance of the dependency resolver.
        /// </summary>
        public DependencyResolver()
        {
            InnerSetResolver(new DefaultDependencyResolver());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the currently set resolver.
        /// </summary>
        public IDependencyResolver InnerCurrent
        {
            get { return _current; }
        }

        /// <summary>
        /// Gets the current cache resolver.
        /// </summary>
        internal static IDependencyResolver CurrentCache
        {
            get { return _instance.InnerCurrentCache; }
        }

        /// <summary>
        /// Gets the resolver that provides caching over results returned by Current.
        /// </summary>
        internal IDependencyResolver InnerCurrentCache
        {
            get { return _currentCache; }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Sets a dependency resolver for the IOC pattern.
        /// </summary>
        /// <param name="resolver">The resolver to use.</param>
        public static void SetResolver(IDependencyResolver resolver)
        {
            _instance.InnerSetResolver(resolver);
        }

        /// <summary>
        /// Tries to set a common service locator as dependency resolver.
        /// The provided object requires methods such as GetInstance or GetAllInstances.
        /// </summary>
        /// <param name="commonServiceLocator"></param>
        public static void SetResolver(Object commonServiceLocator)
        {
            _instance.InnerSetResolver(commonServiceLocator);
        }

        /// <summary>
        /// Tries to set two functions as source for dependency injection. Both functions
        /// need to be supplied.
        /// </summary>
        /// <param name="getService">The function to get a particular service defined by its type.</param>
        /// <param name="getServices">The function to get a list of available services defined by their type.</param>
        public static void SetResolver(Func<Type, Object> getService, Func<Type, IEnumerable<Object>> getServices)
        {
            _instance.InnerSetResolver(getService, getServices);
        }

        #endregion

        #region Methods

        public void InnerSetResolver(IDependencyResolver resolver)
        {
            if (resolver == null)
                throw new ArgumentNullException("resolver");

            _current = resolver;
            _currentCache = new CacheDependencyResolver(_current);
        }

        public void InnerSetResolver(Object commonServiceLocator)
        {
            if (commonServiceLocator == null)
                throw new ArgumentNullException("commonServiceLocator");

            var locatorType = commonServiceLocator.GetType();
            var getInstance = locatorType.GetRuntimeMethod("GetInstance", new[] { typeof(Type) });
            var getInstances = locatorType.GetRuntimeMethod("GetAllInstances", new[] { typeof(Type) });

            if (getInstance == null || getInstance.ReturnType != typeof(Object) || getInstances == null || getInstances.ReturnType != typeof(IEnumerable<Object>))
                throw new ArgumentException("commonServiceLocator");

            var getService = getInstance.CreateDelegate(typeof(Func<Type, Object>), commonServiceLocator) as Func<Type, Object>;
            var getServices = getInstances.CreateDelegate(typeof(Func<Type, IEnumerable<Object>>), commonServiceLocator) as Func<Type, IEnumerable<Object>>;

            InnerSetResolver(new DelegateBasedDependencyResolver(getService, getServices));
        }

        public void InnerSetResolver(Func<Type, Object> getService, Func<Type, IEnumerable<Object>> getServices)
        {
            if (getService == null)
                throw new ArgumentNullException("getService");
            else if (getServices == null)
                throw new ArgumentNullException("getServices");

            InnerSetResolver(new DelegateBasedDependencyResolver(getService, getServices));
        }

        #endregion

        #region Default IOC containers

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

        sealed class DefaultDependencyResolver : IDependencyResolver
        {
            Dictionary<Type, Func<Object>> _items;

            public DefaultDependencyResolver ()
	        {
                var info = new DefaultInfo();
                _items = new Dictionary<Type, Func<Object>>();
                _items.Add(typeof(IHttpRequest), () => new DefaultHttpRequest());
                _items.Add(typeof(IHttpResponse), () => new DefaultHttpResponse());
                _items.Add(typeof(IInfo), () => info);
	        }

            public Object GetService(Type serviceType)
            {
                // Since attempting to create an instance of an interface or an abstract type results in an exception, immediately return null
                // to improve performance and the debugging experience with first-chance exceptions enabled.
                if (_items.ContainsKey(serviceType))
                    return _items[serviceType]();

                if (serviceType.GetTypeInfo().IsInterface || serviceType.GetTypeInfo().IsAbstract)
                    return null;

                try
                {
                    return Activator.CreateInstance(serviceType);
                }
                catch
                {
                    return null;
                }
            }

            public IEnumerable<Object> GetServices(Type serviceType)
            {
                return Enumerable.Empty<Object>();
            }
        }

        sealed class DelegateBasedDependencyResolver : IDependencyResolver
        {
            Func<Type, Object> _getService;
            Func<Type, IEnumerable<Object>> _getServices;

            public DelegateBasedDependencyResolver(Func<Type, Object> getService, Func<Type, IEnumerable<Object>> getServices)
            {
                _getService = getService;
                _getServices = getServices;
            }

            public Object GetService(Type type)
            {
                try
                {
                    return _getService.Invoke(type);
                }
                catch
                {
                    return null;
                }
            }

            public IEnumerable<Object> GetServices(Type type)
            {
                return _getServices(type);
            }
        }

        #endregion
    }
}
