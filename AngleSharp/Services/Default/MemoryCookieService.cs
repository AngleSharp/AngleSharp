namespace AngleSharp.Services.Default
{
    using System;
    using System.Net;

    /// <summary>
    /// Represents the default cookie service. This class can be inherited.
    /// </summary>
    public class MemoryCookieService : ICookieService
    {
        readonly CookieContainer _container;

        /// <summary>
        /// Creates a new cookie service for non-persistent cookies.
        /// </summary>
        public MemoryCookieService()
        {
            _container = new CookieContainer();
        }

        /// <summary>
        /// Gets or sets the cookies for the given origin.
        /// </summary>
        /// <param name="origin">The origin of the cookie.</param>
        /// <returns>The cookie header.</returns>
        public String this[String origin]
        {
            get { return _container.GetCookieHeader(new Uri(origin)); }
            set { _container.SetCookies(new Uri(origin), value); }
        }
    }
}
