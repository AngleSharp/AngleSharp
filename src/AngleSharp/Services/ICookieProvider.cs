namespace AngleSharp.Services
{
    using System;

    /// <summary>
    /// Defines methods to retrieve and store cookies.
    /// </summary>
    public interface ICookieProvider
    {
        /// <summary>
        /// Gets the cookie value of the given address.
        /// </summary>
        /// <param name="origin">The origin of the cookie (Url).</param>
        /// <returns>The value of the cookie.</returns>
        String GetCookie(String origin);

        /// <summary>
        /// Sets the cookie value for the given address.
        /// </summary>
        /// <param name="origin">The origin of the cookie (Url).</param>
        /// <param name="value">The value of the cookie.</param>
        void SetCookie(String origin, String value);
    }
}
