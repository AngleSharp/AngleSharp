using AngleSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AngleSharp
{
    class DependencyResolver
    {
        #region Members

        IDependencyResolver _current;
        CacheDependencyResolver _currentCache;

        #endregion

        #region ctor

        public DependencyResolver()
        {
            InnerSetResolver(new DefaultDependencyResolver());
        }

        public DependencyResolver(DependencyResolver resolver)
        {
            InnerSetResolver(resolver._current);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current resolver.
        /// </summary>
        public IDependencyResolver InnerCurrent
        {
            get { return _current; }
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

            if (getInstance == null ||
                getInstance.ReturnType != typeof(Object) ||
                getInstances == null ||
                getInstances.ReturnType != typeof(IEnumerable<Object>))
            {
                throw new ArgumentException("commonServiceLocator");
            }

            var getService = (Func<Type, Object>)getInstance.CreateDelegate(typeof(Func<Type, Object>), commonServiceLocator);
            var getServices = (Func<Type, IEnumerable<Object>>)getInstances.CreateDelegate(typeof(Func<Type, IEnumerable<Object>>), commonServiceLocator);

            InnerSetResolver(new DelegateBasedDependencyResolver(getService, getServices));
        }

        public void InnerSetResolver(Func<Type, Object> getService, Func<Type, IEnumerable<Object>> getServices)
        {
            if (getService == null)
                throw new ArgumentNullException("getService");

            if (getServices == null)
                throw new ArgumentNullException("getServices");

            InnerSetResolver(new DelegateBasedDependencyResolver(getService, getServices));
        }

        #endregion
    }
}
