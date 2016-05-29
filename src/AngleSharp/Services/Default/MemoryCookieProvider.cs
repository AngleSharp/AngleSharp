namespace AngleSharp.Services.Default
{
    using System;
    using System.Net;

    /// <summary>
    /// Represents the default cookie service. This class can be inherited.
    /// </summary>
    public class MemoryCookieProvider : ICookieProvider
    {
        readonly CookieContainer _container;

        /// <summary>
        /// Creates a new cookie service for non-persistent cookies.
        /// </summary>
        public MemoryCookieProvider()
        {
            _container = new CookieContainer();
        }

        /// <summary>
        /// Gets the associated cookie container.
        /// </summary>
        public CookieContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        /// Gets the cookie value of the given address.
        /// </summary>
        /// <param name="origin">The origin of the cookie (Url).</param>
        /// <returns>The value of the cookie.</returns>
        public String GetCookie(String origin)
        {
            return _container.GetCookieHeader(new Uri(origin));
        }

        /// <summary>
        /// Sets the cookie value for the given address.
        /// </summary>
        /// <param name="origin">The origin of the cookie (Url).</param>
        /// <param name="value">The value of the cookie.</param>
        public void SetCookie(String origin, String value)
        {
            _container.SetCookies(new Uri(origin), value);
        }
    }
}
