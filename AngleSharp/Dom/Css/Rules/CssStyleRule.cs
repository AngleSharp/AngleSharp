namespace AngleSharp.Dom.Css
{
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

        internal CssStyleRule(CssParser parser)
            : base(CssRuleType.Style, parser)
        {
            _style = new CssStyleDeclaration(this);
            _selector = SimpleSelector.All;
            Children = GetChildren();
        }

        #endregion

        #region Properties

        public ISelector Selector
        {
            get { return _selector; }
            set { _selector = value; }
        }

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

        ICssStyleDeclaration ICssStyleRule.Style
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
            var newRule = (CssStyleRule)rule;
            _selector = newRule._selector;
            _style.Clear();
            _style.SetDeclarations(newRule._style.Declarations);
        }

        IEnumerable<ICssNode> GetChildren()
        {
            yield return _selector;
            yield return _style;
        }

        #endregion

        #region String Representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = _style.ToCss(formatter);
            return formatter.Style(_selector.Text, rules);
        }

        #endregion
	}
}
