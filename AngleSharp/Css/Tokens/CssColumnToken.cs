using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// The column token that contains a column (||).
    /// </summary>
    class CssColumnToken : CssToken
    {
        /// <summary>
        /// Creates a new CSS column token.
        /// </summary>
        public CssColumnToken()
        {
            _type = CssTokenType.Column;
        }

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override string ToValue()
        {
            return "||";
        }
    }
}
