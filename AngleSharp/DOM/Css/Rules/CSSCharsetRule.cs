using System;
using System.Text;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS @charset rule.
    /// </summary>
    public sealed class CSSCharsetRule : CSSRule
    {
        #region Constants

        internal const String RuleName = "charset";

        #endregion

        #region ctor

        internal CSSCharsetRule()
        {
            _type = CssRule.Charset;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the encoding information set by this rule.
        /// </summary>
        public String Encoding
        {
            get;
            internal set;
        }

        #endregion
    }
}
