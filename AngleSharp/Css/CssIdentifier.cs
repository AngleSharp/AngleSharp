namespace AngleSharp.DOM.Css
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

        #region Casts

        /// <summary>
        /// Defines an explicit cast from a string to a CssIdentifier.
        /// </summary>
        /// <param name="str">The string to wrap.</param>
        /// <returns>The wrapped string.</returns>
        public static explicit operator CssIdentifier(String str)
        {
            return new CssIdentifier(str);
        }

        /// <summary>
        /// Defines an implicit cast from a CssIdentifier to a string.
        /// </summary>
        /// <param name="str">The string to unwrap.</param>
        /// <returns>The original string.</returns>
        public static implicit operator String(CssIdentifier str)
        {
            return str._token;
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
