namespace AngleSharp
{
    using AngleSharp.Browser;
    using AngleSharp.Browser.Dom;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A simple and lightweight browsing context.
    /// </summary>
    public sealed class BrowsingContext : EventTarget, IBrowsingContext, IDisposable
    {
        #region Fields

        private readonly IEnumerable<Object> _originalServices;
        private readonly List<Object> _services;
        private readonly Sandboxes _security;
        private readonly IBrowsingContext _parent;
        private readonly IDocument _creator;
        private readonly IHistory _history;
        private readonly Dictionary<String, WeakReference<IBrowsingContext>> _children;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new browsing context with the given configuration, or the
        /// default configuration, if no configuration is provided.
        /// </summary>
        /// <remarks>
        /// This constructor was only added due to PowerShell. See #844 for details.
        /// </remarks>
        /// <param name="configuration">The optional configuration.</param>
        public BrowsingContext(IConfiguration configuration = null)
            : this((configuration ?? AngleSharp.Configuration.Default).Services, Sandboxes.None)
        {
        }

        private BrowsingContext(Sandboxes security)
        {
            _services = new List<Object>();
            _originalServices = _services;
            _security = security;
            _children = new Dictionary<String, WeakReference<IBrowsingContext>>();
        }

        internal BrowsingContext(IEnumerable<Object> services, Sandboxes security)
            : this(security)
        {
            _services.AddRange(services);
            _originalServices = services;
            _history = GetService<IHistory>();
        }

        internal BrowsingContext(IBrowsingContext parent, Sandboxes security)
            : this(parent.OriginalServices, security)
        {
            _parent = parent;
            _creator = _parent.Active;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the currently active document.
        /// </summary>
        public IDocument Active
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the document that created the current context, if any. The
        /// creator is the active document of the parent at the time of
        /// creation.
        /// </summary>
        public IDocument Creator => _creator;

        /// <summary>
        /// Gets the original services for the given browsing context.
        /// </summary>
        public IEnumerable<Object> OriginalServices => _originalServices;

        /// <summary>
        /// Gets the current window proxy.
        /// </summary>
        public IWindow Current => Active?.DefaultView;

        /// <summary>
        /// Gets the parent of the current context, if any. If a parent is
        /// available, then the current context contains only embedded
        /// documents.
        /// </summary>
        public IBrowsingContext Parent => _parent;

        /// <summary>
        /// Gets the session history of the given browsing context, if any.
        /// </summary>
        public IHistory SessionHistory => _history;

        /// <summary>
        /// Gets the sandboxing flag of the context.
        /// </summary>
        public Sandboxes Security => _security;

        #endregion

        #region Methods

        /// <summary>
        /// Gets an instance of the given service.
        /// </summary>
        /// <typeparam name="T">The type of service to resolve.</typeparam>
        /// <returns>The instance of the service or null.</returns>
        public T GetService<T>() where T : class
        {
            var count = _services.Count;

            for (var i = 0; i < count; i++)
            {
                var service = _services[i];
                var instance = service as T;

                if (instance == null)
                {
                    if (service is Func<IBrowsingContext, T> creator)
                    {
                        instance = creator.Invoke(this);
                        _services[i] = instance;
                    }
                    else
                    {
                        continue;
                    }
                }

                return instance;
            }

            return null;
        }

        /// <summary>
        /// Gets all registered instances of the given service.
        /// </summary>
        /// <typeparam name="T">The type of service to resolve.</typeparam>
        /// <returns>An enumerable with all service instances.</returns>
        public IEnumerable<T> GetServices<T>() where T : class
        {
            var count = _services.Count;

            for (var i = 0; i < count; i++)
            {
                var service = _services[i];
                var instance = service as T;

                if (instance == null)
                {
                    if (service is Func<IBrowsingContext, T> creator)
                    {
                        instance = creator.Invoke(this);
                        _services[i] = instance;
                    }
                    else
                    {
                        continue;
                    }
                }

                yield return instance;
            }
        }

        /// <summary>
        /// Creates a new named browsing context as child of the given parent.
        /// </summary>
        /// <param name="name">The name of the child context, if any.</param>
        /// <param name="security">The security flags to apply.</param>
        /// <returns></returns>
        public IBrowsingContext CreateChild(String name, Sandboxes security)
        {
            var context = new BrowsingContext(this, security);

            if (!String.IsNullOrEmpty(name))
            {
                _children[name] = new WeakReference<IBrowsingContext>(context);
            }

            return context;
        }

        /// <summary>
        /// Finds a named browsing context.
        /// </summary>
        /// <param name="name">The name of the browsing context.</param>
        /// <returns>The found instance, if any.</returns>
        public IBrowsingContext FindChild(String name)
        {
            var context = default(IBrowsingContext);

            if (!String.IsNullOrEmpty(name) && _children.TryGetValue(name, out var reference))
            {
                reference.TryGetTarget(out context);
            }

            return context;
        }

        /// <summary>
        /// Creates a new browsing context with the given configuration, or the
        /// default configuration, if no configuration is provided.
        /// </summary>
        /// <param name="configuration">The optional configuration.</param>
        /// <returns>The browsing context to use.</returns>
        public static IBrowsingContext New(IConfiguration configuration = null)
        {
            if (configuration == null)
            {
                configuration = AngleSharp.Configuration.Default;
            }

            return new BrowsingContext(configuration.Services, Sandboxes.None);
        }

        /// <summary>
        /// Creates a new browsing context from the given service.
        /// </summary>
        /// <param name="instance">The service instance.</param>
        /// <returns>The browsing context to use.</returns>
        public static IBrowsingContext NewFrom<TService>(TService instance)
        {
            var configuration = Configuration.Default.WithOnly<TService>(instance);
            return new BrowsingContext(configuration.Services, Sandboxes.None);
        }

        void IDisposable.Dispose()
        {
            Active?.Dispose();
            Active = null;
        }

        #endregion
    }
}
