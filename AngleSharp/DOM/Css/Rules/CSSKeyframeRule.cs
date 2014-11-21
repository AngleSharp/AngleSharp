namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents a CSS @keyframe rule.
    /// </summary>
    sealed class CSSKeyframeRule : CSSRule, ICssKeyframeRule
    {
        #region Fields

        readonly CSSStyleDeclaration _style;
        IKeyframeSelector _selector;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @keyframe rule.
        /// </summary>
        internal CSSKeyframeRule()
        {
            _style = new CSSStyleDeclaration(this);
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
            get { return _selector.Text; }
            set
            {
                var selector = CssParser.ParseKeyText(value);

                if (selector == null)
                    throw new DomException(ErrorCode.Syntax);

                _selector = selector;
            }
        }

        /// <summary>
        /// Gets or sets the selector for matching elements.
        /// </summary>
        public IKeyframeSelector Key
        {
            get { return _selector; }
            set { _selector = value; }
        }

        /// <summary>
        /// Gets a CSSStyleDeclaration of the CSS style associated with the key from.
        /// </summary>
        ICssStyleDeclaration ICssKeyframeRule.Style
        {
            get { return _style; }
        }

        public CSSStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSKeyframeRule;
            _selector = newRule._selector;
            _style.TakeFrom(newRule._style);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Concat(KeyText, " ", _style.ToCssBlock());
        }

        #endregion
    }
}
