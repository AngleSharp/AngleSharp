using AngleSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AngleSharp
{
    /// <summary>
    /// Represents global configuration for the AngleSharp library.
    /// This is independent of parsing / document creation options
    /// given in the DocumentOptions class.
    /// </summary>
    public sealed class Configuration
    {
        #region Singleton

        static Configuration instance;

        static Configuration()
        {
            Reset();
        }

        #endregion

        #region Members

        List<Type> requester;
        DependencyResolver resolver;
        CultureInfo culture;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new default configuration.
        /// </summary>
        Configuration()
        {
            requester = new List<Type>();
            culture = CultureInfo.CurrentUICulture;
            resolver = new DependencyResolver();
        }

        /// <summary>
        /// Copies the new configuration from the given one.
        /// </summary>
        /// <param name="config">The configuration to clone.</param>
        Configuration(Configuration config)
        {
            requester = new List<Type>(config.requester);
            culture = config.culture;
            resolver = new DependencyResolver(config.resolver);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current dependency resolver.
        /// </summary>
        public static IDependencyResolver CurrentResolver
        {
            get { return instance.resolver.InnerCurrent; }
        }

        /// <summary>
        /// Gets if at least one HttpRequester has been registered.
        /// </summary>
        public static Boolean HasHttpRequester
        {
            get { return instance.requester.Count > 0; }
        }

#if !LEGACY
        /// <summary>
        /// Gets or sets if the default Http requester should be used.
        /// </summary>
        public static Boolean UseDefaultHttpRequester
        {
            get { return instance.requester.Contains(typeof(DefaultHttpRequester)); }
            set
            {
                var current = UseDefaultHttpRequester;

                if (current != value)
                {
                    if (value)
                        RegisterHttpRequester<DefaultHttpRequester>();
                    else
                        UnregisterHttpRequester<DefaultHttpRequester>();
                }
            }
        }
#endif

        /// <summary>
        /// Gets or sets the language (code, e.g. en-US, de-DE) to use.
        /// </summary>
        public static String Language
        {
            get { return instance.culture.Name; }
            set { Culture = new CultureInfo(value); }
        }

        /// <summary>
        /// Gets or sets the culture to use.
        /// </summary>
        public static CultureInfo Culture
        {
            get { return instance.culture; }
            set { instance.culture = value ?? CultureInfo.CurrentUICulture; }
        }

        #endregion

        #region Http requester

        /// <summary>
        /// Gets a fresh HTTP requester from the first type that can be created.
        /// </summary>
        /// <returns>The created HTTP requester instance or null.</returns>
        public static IHttpRequester GetHttpRequester()
        {
            for (int i = 0; i < instance.requester.Count; i++)
            {
                var result = CurrentResolver.GetService(instance.requester[i]) as IHttpRequester;

                if (result == null)
                    continue;

                return result;
            }

            return null;
        }

        /// <summary>
        /// Registers a new HttpRequester for making resource requests.
        /// </summary>
        /// <typeparam name="T">The type of the requester.</typeparam>
        public static void RegisterHttpRequester<T>()
            where T: IHttpRequester
        {
            var requester = typeof(T);

            if(!instance.requester.Contains(requester))
                instance.requester.Insert(0, requester);
        }

        /// <summary>
        /// Removes a registered HttpRequester for making resource requests.
        /// </summary>
        /// <typeparam name="T">The type of the requester.</typeparam>
        public static void UnregisterHttpRequester<T>()
            where T : IHttpRequester
        {
            var requester = typeof(T);

            if (instance.requester.Contains(requester))
                instance.requester.Remove(requester);
        }

        #endregion

        #region IoC container

        /// <summary>
        /// Sets the dependency resolver to the given one.
        /// </summary>
        /// <param name="resolver">The resolver to use.</param>
        public static void SetDependencyResolver(IDependencyResolver resolver)
        {
            instance.resolver.InnerSetResolver(resolver);
        }

        /// <summary>
        /// Sets the dependency resolver to the given common service locator object.
        /// </summary>
        /// <param name="commonServiceLocator">The common service locator to use.</param>
        public static void SetDependencyResolver(Object commonServiceLocator)
        {
            instance.resolver.InnerSetResolver(commonServiceLocator);
        }

        /// <summary>
        /// Sets the dependency resolver to use the given methods for single and
        /// multiple services.
        /// </summary>
        /// <param name="getService">The method to use for resolving a single service.</param>
        /// <param name="getServices">The method to use for resolving multiple services.</param>
        public static void SetDependencyResolver(Func<Type, Object> getService, Func<Type, IEnumerable<Object>> getServices)
        {
            instance.resolver.InnerSetResolver(getService, getServices);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets the current configuration to the initial 
        /// configuration.
        /// </summary>
        public static void Reset()
        {
            instance = new Configuration();
        }

        /// <summary>
        /// Loads the specified configuration, overwriting the current one.
        /// </summary>
        /// <param name="config">The configuration to load.</param>
        public static void Load(Configuration config)
        {
            instance = config;
        }

        /// <summary>
        /// Saves the current configuration to be used later.
        /// </summary>
        /// <returns>The saved configuration state.</returns>
        public static Configuration Save()
        {
            var old = instance;
            instance = new Configuration(old);
            return old;
        }

        #endregion
    }
}
