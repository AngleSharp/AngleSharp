using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS @keyframe rule.
    /// </summary>
    [DOM("CSSKeyframeRule")]
    public sealed class CSSKeyframeRule : CSSRule
    {
        #region Members

        String keyText;
        CSSStyleDeclaration style;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @keyframe rule.
        /// </summary>
        internal CSSKeyframeRule()
        {
            style = new CSSStyleDeclaration();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the key of the keyframe, like '10%', '75%'. The from keyword maps to '0%' and the to keyword maps to '100%'.
        /// </summary>
        [DOM("keyText")]
        public String KeyText
        {
            get { return keyText; }
            set { keyText = value; }
        }

        /// <summary>
        /// Gets a CSSStyleDeclaration of the CSS style associated with the key from.
        /// </summary>
        [DOM("style")]
        public CSSStyleDeclaration Style
        {
            get { return style; }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return keyText + " {" + Environment.NewLine + style.ToCss() + "}";
        }

        #endregion
    }
}
