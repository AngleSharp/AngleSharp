namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents an URL to define an image.
    /// </summary>
    sealed class ImageUrl : IImageSource
    {
        readonly CssUrl _url;

        /// <summary>
        /// Creates a new image URL.
        /// </summary>
        /// <param name="url">The URL to use.</param>
        public ImageUrl(CssUrl url)
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
            get { return ((ICssValue)_url).CssText; }
        }

        #endregion
    }
}
