namespace AngleSharp.Extensions
{
    using AngleSharp.Network;
    using System;

    /// <summary>
    /// Represents some useful extensions for the response.
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Gets the content-type from the response's headers. The default type
        /// is derived from the file extension of the path, if any.
        /// </summary>
        /// <param name="response">The response to examine.</param>
        /// <returns>The provided or default content-type.</returns>
        public static MimeType GetContentType(this IResponse response)
        {
            var fileName = response.Address.Path;
            var index = fileName.LastIndexOf('.');
            var extension = index >= 0 ? fileName.Substring(index) : ".a";
            var defaultType = MimeTypeNames.FromExtension(extension);
            var value = response.Headers.GetOrDefault(HeaderNames.ContentType, defaultType);
            return new MimeType(value);
        }

        /// <summary>
        /// Gets the content-type from the response's headers.
        /// </summary>
        /// <param name="response">The response to examine.</param>
        /// <param name="defaultType">The default type to apply.</param>
        /// <returns>The provided or default content-type.</returns>
        public static MimeType GetContentType(this IResponse response, String defaultType)
        {
            var fileName = response.Address.Path;
            var index = fileName.LastIndexOf('.');

            if (index >= 0)
            {
                var extension = fileName.Substring(index);
                defaultType = MimeTypeNames.FromExtension(extension);
            }

            var value = response.Headers.GetOrDefault(HeaderNames.ContentType, defaultType);
            return new MimeType(value);
        }
    }
}
