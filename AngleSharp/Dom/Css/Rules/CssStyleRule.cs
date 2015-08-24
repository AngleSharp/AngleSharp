namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS style rule.
    /// </summary>
	sealed class CssStyleRule : CssRule, ICssStyleRule
    {
        #region Fields

        readonly CssStyleDeclaration _style;
        ISelector _selector;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS style rule.
        /// </summary>
        internal CssStyleRule(CssParser parser)
            : base(CssRuleType.Style, parser)
        {
            _style = new CssStyleDeclaration(this);
            _selector = SimpleSelector.All;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the selector for matching elements.
        /// </summary>
        public ISelector Selector
        {
            get { return _selector; }
            set { _selector = value; }
        }

        /// <summary>
        /// Gets or sets the textual representation of the selector for this rule, e.g. "h1,h2".
        /// </summary>
        public String SelectorText
        {
            get { return _selector.Text; }
            set 
            {
                var selector = Parser.ParseSelector(value);

                if (selector != null)
                    _selector = selector;
            }
        }

        /// <summary>
        /// Gets the CSSStyleDeclaration object for the rule.
        /// </summary>
        ICssStyleDeclaration ICssStyleRule.Style
        {
            get { return _style; }
        }

        public CssStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        public override String GetSource()
        {
            var source = String.Concat(_selector.Text, "{", _style.GetSource(), "}");
            return Decorate(source);
        }

        public override IEnumerable<CssNode> GetChildren()
        {
            yield return _style;
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (CssStyleRule)rule;
            _selector = newRule._selector;
            _style.Clear();
            _style.SetDeclarations(newRule._style.Declarations);
        }

        #endregion

        #region String representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = _style.ToCss(formatter);
            return formatter.Style(_selector.Text, rules);
        }

        #endregion
	}
}
