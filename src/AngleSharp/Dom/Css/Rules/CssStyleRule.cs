namespace AngleSharp.Dom.Css
{
    using AngleSharp.Parser.Css;
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a CSS style rule.
    /// </summary>
	sealed class CssStyleRule : CssRule, ICssStyleRule
    {
        #region ctor

        internal CssStyleRule(CssParser parser)
            : base(CssRuleType.Style, parser)
        {
            AppendChild(SimpleSelector.All);
            AppendChild(new CssStyleDeclaration(this));
        }

        #endregion

        #region Properties

        public ISelector Selector
        {
            get { return Children.OfType<ISelector>().FirstOrDefault(); }
            set { ReplaceSingle(Selector, value); }
        }

        public String SelectorText
        {
            get { return Selector.Text; }
            set { Selector = Parser.ParseSelector(value); }
        }

        ICssStyleDeclaration ICssStyleRule.Style
        {
            get { return Style; }
        }

        public CssStyleDeclaration Style
        {
            get { return Children.OfType<CssStyleDeclaration>().FirstOrDefault(); }
        }

        #endregion

        #region String Representation

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Style(SelectorText, Style));
        }

        #endregion
	}
}
