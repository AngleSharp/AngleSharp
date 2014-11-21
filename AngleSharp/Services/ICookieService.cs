namespace AngleSharp.Services
{
    using System;

    /// <summary>
    /// Defines methods to evaluate and store cookies.
    /// </summary>
    public interface ICookieService : IService
    {
        /// <summary>
        /// Gets or sets the cookie value for the given address.
        /// </summary>
        /// <param name="origin">The origin of the cookie (Url).</param>
        /// <returns>The value of the cookie.</returns>
        String this[String origin] { get; set; }
    }
}
