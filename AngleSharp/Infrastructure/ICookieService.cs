namespace AngleSharp.Infrastructure
{
    using AngleSharp.Network;
    using System;

    /// <summary>
    /// Defines methods to evaluate and store cookies.
    /// </summary>
    public interface ICookieService : IService
    {
        /// <summary>
        /// Gets the cookie from the given response.
        /// </summary>
        /// <param name="response">The response to investigate.</param>
        /// <returns>The string representing the content of the cookie.</returns>
        String GetCookie(IResponse response);

        /// <summary>
        /// Sets the cookie in the given request.
        /// </summary>
        /// <param name="request">The request to modify.</param>
        /// <param name="value">The cookie to set.</param>
        void SetCookie(IRequest request, String value);
    }
}
