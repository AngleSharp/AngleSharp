using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// The base class token for the CSS parser.
    /// </summary>
    abstract class CssToken
    {
        #region Members

        protected CssTokenType _type;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        public CssTokenType Type
        {
            get { return _type; }
        }

        #endregion

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public abstract string ToValue();
    }
}
