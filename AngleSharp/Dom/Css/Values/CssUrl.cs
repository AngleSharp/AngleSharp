namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Wraps a string as a CSS url value.
    /// </summary>
    sealed class CssUrl : Url, ICssValue, IImageSource
    {
        #region Fields

        readonly String _url;

        #endregion

        #region ctor

        /// <summary>
        /// Wraps the given string.
        /// </summary>
        /// <param name="url">The specified url.</param>
        public CssUrl(String url)
            : base(url)
        {
            _url = url;
        }

        #endregion

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
