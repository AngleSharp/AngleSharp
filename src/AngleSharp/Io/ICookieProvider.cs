namespace AngleSharp.Io
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
        /// <param name="url">The origin of the cookie.</param>
        /// <returns>The value of the cookie.</returns>
        String GetCookie(Url url);

        /// <summary>
        /// Sets the cookie value for the given address.
        /// </summary>
        /// <param name="url">The origin of the cookie.</param>
        /// <param name="value">The value of the cookie.</param>
        void SetCookie(Url url, String value);
    }
}
