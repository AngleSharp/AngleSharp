namespace AngleSharp.Dom.Css
{
    using AngleSharp.Parser.Css;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the @page rule.
    /// </summary>
    sealed class CssPageRule : CssRule, ICssPageRule
    {
        #region ctor

        internal CssPageRule(CssParser parser)
            : base(CssRuleType.Page, parser)
        {
            AppendChild(SimpleSelector.All);
            AppendChild(new CssStyleDeclaration(this));
        }

        #endregion

        #region Properties

        public String SelectorText
        {
            get { return Selector.Text; }
            set { Selector = Parser.ParseSelector(value); }
        }

        public ISelector Selector
        {
            get { return Children.OfType<ISelector>().FirstOrDefault(); }
            set { ReplaceSingle(Selector, value); }
        }

        ICssStyleDeclaration ICssPageRule.Style
        {
            get { return Style; }
        }

        public CssStyleDeclaration Style
        {
            get { return Children.OfType<CssStyleDeclaration>().FirstOrDefault(); }
        }

        #endregion

        #region String representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(Style);
            return formatter.Rule("@page", SelectorText, rules);
        }

        #endregion
    }
}
