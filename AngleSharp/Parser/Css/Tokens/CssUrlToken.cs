namespace AngleSharp.Parser.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents a CSS URL token.
    /// </summary>
    sealed class CssUrlToken : CssToken
    {
        #region Fields

        readonly Boolean _bad;
        readonly String _functionName;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS URL token.
        /// </summary>
        /// <param name="functionName">The called function name.</param>
        /// <param name="data">The string data.</param>
        /// <param name="bad">If the string was bad (optional).</param>
        /// <param name="position">The token's position.</param>
        public CssUrlToken(String functionName, String data, Boolean bad, TextPosition position)
            : base(CssTokenType.Url, data, position)
        {
            _bad = bad;
            _functionName = functionName;
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
        /// Gets the name of the used function.
        /// </summary>
        public String FunctionName
        {
            get { return _functionName; }
        }

        #endregion

        #region String representation

        public override String ToValue()
        {
            return String.Concat(_functionName, "(", Data.CssString(), ")");
        }

        #endregion
    }
}
