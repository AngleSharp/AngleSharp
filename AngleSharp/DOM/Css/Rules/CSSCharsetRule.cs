using System;
using System.Text;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS @charset rule.
    /// </summary>
    [DOM("CSSCharsetRule")]
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
        [DOM("encoding")]
        public String Encoding
        {
            get;
            internal set;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Format("@charset '{0}';", Encoding);
        }

        #endregion
    }
}
