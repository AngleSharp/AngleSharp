namespace AngleSharp
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Wraps a string as a CSS url value.
    /// </summary>
    sealed class CssUrl : ICssObject
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
        {
            _url = url;
        }

        #endregion

        #region Casts

        /// <summary>
        /// Defines an explicit cast from a string to a CssUrl.
        /// </summary>
        /// <param name="str">The string to wrap.</param>
        /// <returns>The wrapped string.</returns>
        public static explicit operator CssUrl(String str)
        {
            return new CssUrl(str);
        }

        /// <summary>
        /// Defines an implicit cast from a CssUrl to a string.
        /// </summary>
        /// <param name="str">The string to unwrap.</param>
        /// <returns>The original string.</returns>
        public static implicit operator String(CssUrl str)
        {
            return str._url;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the CSS standard represenation of the contained string.
        /// </summary>
        /// <returns>A string that contains the CSS code to create the value.</returns>
        public String ToCss()
        {
            return _url.CssUrl();
        }

        #endregion
    }
}
