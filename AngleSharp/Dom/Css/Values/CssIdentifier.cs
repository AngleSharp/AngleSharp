namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Wraps a string as a CSS identifier value.
    /// </summary>
    sealed class CssIdentifier : ICssValue
    {
        #region Fields

        readonly String _token;

        #endregion

        #region ctor

        /// <summary>
        /// Wraps the given string.
        /// </summary>
        /// <param name="token">The identifier token.</param>
        public CssIdentifier(String token)
        {
            _token = token.ToLowerInvariant();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the stored value.
        /// </summary>
        public String Value
        {
            get { return _token; }
        }

        #endregion

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get { return _token; }
        }

        #endregion
    }
}
