namespace AngleSharp.Services.Default
{
    using System;
    using System.Globalization;
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
            var cookies = Sanatize(value);
            _container.SetCookies(new Uri(origin), cookies);
        }

        private static String Sanatize(String cookie)
        {
            var expires = "expires=";
            var start = 0;

            while (start < cookie.Length)
            {
                var index = cookie.IndexOf(expires, start, StringComparison.OrdinalIgnoreCase);

                if (index != -1)
                {
                    var position = index + expires.Length;
                    var end = cookie.IndexOfAny(new[] { ';', ',' }, position + 4);

                    if (end == -1)
                    {
                        end = cookie.Length;
                    }

                    var front = cookie.Substring(0, position);
                    var middle = cookie.Substring(position, end - position);
                    var back = cookie.Substring(end);
                    var utc = DateTime.Now;

                    if (DateTime.TryParse(middle.Replace("UTC", "GMT"), out utc))
                    {
                        var time = utc.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        cookie = $"{front}{time}{back}";
                    }

                    start = end;
                }
                else
                {
                    break;
                }
            }

            return cookie;
        }
    }
}
