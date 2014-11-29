namespace AngleSharp.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents an URL to define an image.
    /// </summary>
    public sealed class ImageUrl : IImageSource
    {
        readonly String _url;

        /// <summary>
        /// Creates a new image URL.
        /// </summary>
        /// <param name="url">The URL to use.</param>
        public ImageUrl(String url)
        {
            _url = url;
        }

        /// <summary>
        /// Returns the CSS representation of the linear gradient function.
        /// </summary>
        /// <returns>A string that resembles CSS code.</returns>
        public String ToCss()
        {
            return _url.CssUrl();
        }
    }
}
