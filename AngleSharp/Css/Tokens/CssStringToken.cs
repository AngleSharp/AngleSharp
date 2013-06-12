using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a CSS string token.
    /// </summary>
    sealed class CssStringToken : CssToken
    {
        #region Members

        string _data;
        bool _bad;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS string token.
        /// </summary>
        /// <param name="type">The exact type.</param>
        private CssStringToken(CssTokenType type)
        {
            _type = type;
        }

        /// <summary>
        /// Creates a new CSS string token (plain string).
        /// </summary>
        /// <param name="data">The string data.</param>
        /// <param name="bad">If the string was bad (optional).</param>
        /// <returns>The created string token.</returns>
        public static CssStringToken Plain(string data, bool bad = false)
        {
            return new CssStringToken(CssTokenType.String) { _data = data, _bad = bad };
        }

        /// <summary>
        /// Creates a new CSS string token (URL string).
        /// </summary>
        /// <param name="data">The URL string data.</param>
        /// <param name="bad">If the URL was bad (optional).</param>
        /// <returns>The created URL string token.</returns>
        public static CssStringToken Url(string data, bool bad = false)
        {
            return new CssStringToken(CssTokenType.Url) { _data = data, _bad = bad };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the contained data.
        /// </summary>
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Gets or sets if the data is bad.
        /// </summary>
        public bool IsBad
        {
            get { return _bad; }
            set { _bad = value; }
        }

        #endregion

        #region string representation

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override string ToValue()
        {
            switch(_type)
            {
                case CssTokenType.Url:
                    return "url('" + _data + "')";
                default:
                    return "'" + _data + "'";
            }
        }

        #endregion
    }
}
