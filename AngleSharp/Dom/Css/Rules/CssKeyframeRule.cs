namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents a CSS @keyframe rule.
    /// </summary>
    sealed class CssKeyframeRule : CssRule, ICssKeyframeRule
    {
        #region Fields

        readonly CssStyleDeclaration _style;
        IKeyframeSelector _selector;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @keyframe rule.
        /// </summary>
        internal CssKeyframeRule()
            : base(CssRuleType.Keyframe)
        {
            _style = new CssStyleDeclaration(this);
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
                    throw new DomException(DomError.Syntax);

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

        public CssStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (CssKeyframeRule)rule;
            _selector = newRule._selector;
            _style.Clear();
            _style.SetDeclarations(newRule._style);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected override String ToCss()
        {
            return String.Concat(KeyText, " ", _style.ToCssBlock());
        }

        #endregion
    }
}
