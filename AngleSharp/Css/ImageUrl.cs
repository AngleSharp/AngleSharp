namespace AngleSharp.DOM.Css
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

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get { return _url.CssUrl(); }
        }

        #endregion
    }
}
