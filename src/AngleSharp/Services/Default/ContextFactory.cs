namespace AngleSharp.Services.Default
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// THe default browsing context factory.
    /// </summary>
    public class ContextFactory : IContextFactory
    {
        readonly Dictionary<String, WeakReference<IBrowsingContext>> _cache = new Dictionary<String, WeakReference<IBrowsingContext>>();

        /// <summary>
        /// Creates a new browsing context from the given configuration.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="security">The security flags to apply.</param>
        /// <returns></returns>
        public IBrowsingContext Create(IConfiguration configuration, Sandboxes security)
        {
            return new BrowsingContext(configuration, security);
        }

        /// <summary>
        /// Creates a new named browsing context as child of the given parent.
        /// </summary>
        /// <param name="parent">The parent context.</param>
        /// <param name="name">The name of the child context.</param>
        /// <param name="security">The security flags to apply.</param>
        /// <returns></returns>
        public IBrowsingContext Create(IBrowsingContext parent, String name, Sandboxes security)
        {
            var context = new BrowsingContext(parent, security);
            _cache[name] = new WeakReference<IBrowsingContext>(context);
            return context;
        }

        /// <summary>
        /// Finds a named browsing context.
        /// </summary>
        /// <param name="name">The name of the browsing context.</param>
        /// <returns>The found instance, if any.</returns>
        public IBrowsingContext Find(String name)
        {
            var cached = default(WeakReference<IBrowsingContext>);
            var context = default(IBrowsingContext);

            if (_cache.TryGetValue(name, out cached))
            {
                cached.TryGetTarget(out context);
            }

            return context;
        }
    }
}
