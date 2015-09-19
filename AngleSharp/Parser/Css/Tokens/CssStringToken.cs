namespace AngleSharp.Parser.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents a CSS string token.
    /// </summary>
    sealed class CssStringToken : CssToken
    {
        #region Fields

        readonly Boolean _bad;
        readonly Char _quote;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS string token.
        /// </summary>
        /// <param name="data">The string data.</param>
        /// <param name="bad">Was the string ended prematurely.</param>
        /// <param name="quote">The used quote symbol.</param>
        /// <param name="position">The token's position.</param>
        public CssStringToken(String data, Boolean bad, Char quote, TextPosition position)
            : base(CssTokenType.String, data, position)
        {
            _bad = bad;
            _quote = quote;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the data is bad.
        /// </summary>
        public Boolean IsBad
        {
            get { return _bad; }
        }

        /// <summary>
        /// Gets the used quote symbol.
        /// </summary>
        public Char Quote
        {
            get { return _quote; }
        }

        #endregion

        #region String representation

        public override String ToValue()
        {
            return Data.CssString();
        }

        #endregion
    }
}
