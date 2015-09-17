namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// Represents a CSS color token.
    /// </summary>
    sealed class CssColorToken : CssToken
    {
        #region ctor

        /// <summary>
        /// Creates a new CSS color token.
        /// </summary>
        /// <param name="data">The color data.</param>
        /// <param name="position">The token's position.</param>
        public CssColorToken(String data, TextPosition position)
            : base(CssTokenType.Color, data, position)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the data is bad.
        /// </summary>
        public Boolean IsBad
        {
            get { return Data.Length != 3 && Data.Length != 6; }
        }

        #endregion

        #region String representation

        public override String ToValue()
        {
            return "#" + Data;
        }

        #endregion
    }
}
