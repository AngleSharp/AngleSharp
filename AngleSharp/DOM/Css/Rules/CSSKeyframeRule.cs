namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a CSS @keyframe rule.
    /// </summary>
    sealed class CSSKeyframeRule : CSSRule, ICssKeyframeRule
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
            : this(new CSSStyleDeclaration())
        {
        }

        /// <summary>
        /// Creates a new @keyframe rule.
        /// </summary>
        /// <param name="style">The declaration to use.</param>
        internal CSSKeyframeRule(CSSStyleDeclaration style)
        {
            _style = style;
            _style.Parent = this;
            _type = CssRuleType.Keyframe;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the key of the keyframe, like '10%', '75%'. 
        /// The from keyword maps to '0%' and the to keyword maps to '100%'.
        /// </summary>
        public String KeyText
        {
            get { return _keyText; }
            set
            { 
                //If keyText is updated with an invalid keyframe selector, a SyntaxError exception must be thrown.
                _keyText = value; 
            }
        }

        /// <summary>
        /// Gets a CSSStyleDeclaration of the CSS style associated with the key from.
        /// </summary>
        public ICssStyleDeclaration Style
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
