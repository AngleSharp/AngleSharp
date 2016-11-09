namespace AngleSharp.Io
{
    using System;
    using System.Net;

    /// <summary>
    /// Represents the default cookie service. This class can be inherited.
    /// </summary>
    public class MemoryCookieProvider : ICookieProvider
    {
        private readonly CookieContainer _container;

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
        /// <param name="url">The origin of the cookie.</param>
        /// <returns>The value of the cookie.</returns>
        public String GetCookie(Url url)
        {
            var uri = new Uri(url.Origin);
            return _container.GetCookieHeader(uri);
        }

        /// <summary>
        /// Sets the cookie value for the given address.
        /// </summary>
        /// <param name="url">The origin of the cookie.</param>
        /// <param name="value">The value of the cookie.</param>
        public void SetCookie(Url url, String value)
        {
            var uri = new Uri(url.Origin);
            _container.SetCookies(uri, value);
        }
    }
}
