using System;
using System.Text;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS @charset rule.
    /// </summary>
    [DomName("CSSCharsetRule")]
    public sealed class CSSCharsetRule : CSSRule
    {
        #region ctor

        internal CSSCharsetRule()
        {
            _type = CssRuleType.Charset;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the encoding information set by this rule.
        /// </summary>
        [DomName("encoding")]
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
