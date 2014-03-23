namespace AngleSharp
{
    using AngleSharp.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Represents the dependency resolver for supplying
    /// inversion of control.
    /// </summary>
    public sealed class DependencyResolver
    {
        #region Fields

        static readonly DependencyResolver _resolver;
        IDependencyResolver _current;
        CacheDependencyResolver _currentCache;

        #endregion

        #region ctor

        static DependencyResolver()
        {
            _resolver = new DependencyResolver();
            SetResolver(new DefaultDependencyResolver());
        }

        DependencyResolver() { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current resolver.
        /// </summary>
        public static IDependencyResolver Current
        {
            get { return _resolver._current; }
        }

        /// <summary>
        /// Gets the resolver that provides caching over results returned by Current.
        /// </summary>
        internal IDependencyResolver InnerCurrentCache
        {
            get { return _currentCache; }
        }

        #endregion

        #region Methods

        public static void SetResolver(IDependencyResolver resolver)
        {
            if (resolver == null)
                throw new ArgumentNullException("resolver");

            _resolver._current = resolver;
            _resolver._currentCache = new CacheDependencyResolver(_resolver._current);
        }

        public static void SetResolver(Object commonServiceLocator)
        {
            if (commonServiceLocator == null)
                throw new ArgumentNullException("commonServiceLocator");

            var locatorType = commonServiceLocator.GetType();
            var getInstance = locatorType.GetRuntimeMethod("GetInstance", new[] { typeof(Type) });
            var getInstances = locatorType.GetRuntimeMethod("GetAllInstances", new[] { typeof(Type) });

            if (getInstance == null || getInstance.ReturnType != typeof(Object) || getInstances == null || getInstances.ReturnType != typeof(IEnumerable<Object>))
                throw new ArgumentException("commonServiceLocator");

            var getService = (Func<Type, Object>)getInstance.CreateDelegate(typeof(Func<Type, Object>), commonServiceLocator);
            var getServices = (Func<Type, IEnumerable<Object>>)getInstances.CreateDelegate(typeof(Func<Type, IEnumerable<Object>>), commonServiceLocator);

            SetResolver(new DelegateBasedDependencyResolver(getService, getServices));
        }

        public static void SetResolver(Func<Type, Object> getService, Func<Type, IEnumerable<Object>> getServices)
        {
            if (getService == null)
                throw new ArgumentNullException("getService");

            if (getServices == null)
                throw new ArgumentNullException("getServices");

            SetResolver(new DelegateBasedDependencyResolver(getService, getServices));
        }

        #endregion
    }
}
