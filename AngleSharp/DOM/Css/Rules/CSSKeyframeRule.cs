namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a CSS @keyframe rule.
    /// </summary>
    [DomName("CSSKeyframeRule")]
    sealed class CSSKeyframeRule : CSSRule
    {
        #region Fields

        String _keyText;
        CSSStyleDeclaration _style;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @keyframe rule.
        /// </summary>
        internal CSSKeyframeRule()
        {
            _style = new CSSStyleDeclaration();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the key of the keyframe, like '10%', '75%'. The from keyword maps to '0%' and the to keyword maps to '100%'.
        /// </summary>
        [DomName("keyText")]
        public String KeyText
        {
            get { return _keyText; }
            set { _keyText = value; }
        }

        /// <summary>
        /// Gets a CSSStyleDeclaration of the CSS style associated with the key from.
        /// </summary>
        [DomName("style")]
        public CSSStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSKeyframeRule;
            _keyText = newRule._keyText;
            _style = newRule._style;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return _keyText + " {" + Environment.NewLine + _style.ToCss() + "}";
        }

        #endregion
    }
}
