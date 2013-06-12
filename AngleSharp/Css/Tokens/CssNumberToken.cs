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

        Single _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS number token.
        /// </summary>
        /// <param name="number">The number to contain.</param>
        public CssNumberToken(Single number)
        {
            _type = CssTokenType.Number;
            _data = number;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained number.
        /// </summary>
        public Single Data
        {
            get { return _data; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            return _data.ToString("0.0", CultureInfo.InvariantCulture);
        }

        #endregion
    }
}
