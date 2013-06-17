using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a CSS string token.
    /// </summary>
    sealed class CssStringToken : CssToken
    {
        #region Members

        String _data;
        Boolean _bad;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS string token.
        /// </summary>
        /// <param name="type">The exact type.</param>
        CssStringToken(CssTokenType type)
        {
            _type = type;
        }

        /// <summary>
        /// Creates a new CSS string token (plain string).
        /// </summary>
        /// <param name="data">The string data.</param>
        /// <param name="bad">If the string was bad (optional).</param>
        /// <returns>The created string token.</returns>
        public static CssStringToken Plain(String data, Boolean bad = false)
        {
            return new CssStringToken(CssTokenType.String) { _data = data, _bad = bad };
        }

        /// <summary>
        /// Creates a new CSS string token (URL string).
        /// </summary>
        /// <param name="data">The URL string data.</param>
        /// <param name="bad">If the URL was bad (optional).</param>
        /// <returns>The created URL string token.</returns>
        public static CssStringToken Url(String data, Boolean bad = false)
        {
            return new CssStringToken(CssTokenType.Url) { _data = data, _bad = bad };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained data.
        /// </summary>
        public String Data
        {
            get { return _data; }
        }

        /// <summary>
        /// Gets if the data is bad.
        /// </summary>
        public Boolean IsBad
        {
            get { return _bad; }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            if(_type == CssTokenType.Url)
                return "url('" + _data + "')";

            return "'" + _data + "'";
        }

        #endregion
    }
}
