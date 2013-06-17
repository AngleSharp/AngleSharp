using System;
using System.Globalization;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a CSS number token.
    /// </summary>
    sealed class CssNumberToken : CssToken
    {
        #region Members

        String _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS number token.
        /// </summary>
        /// <param name="number">The number to contain.</param>
        public CssNumberToken(String number)
        {
            _data = number;
            _type = CssTokenType.Number;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained number.
        /// </summary>
        public Single Data
        {
            get { return Single.Parse(_data, CultureInfo.InvariantCulture); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            return _data;
        }

        #endregion
    }
}
