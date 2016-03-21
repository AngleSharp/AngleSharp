namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.IO;

    /// <summary>
    /// Represents the CSS @charset rule.
    /// </summary>
    sealed class CssCharsetRule : CssRule, ICssCharsetRule
    {
        #region ctor

        internal CssCharsetRule(CssParser parser)
            : base(CssRuleType.Charset, parser)
        {
        }

        #endregion

        #region Properties

        public String CharacterSet
        {
            get;
            set;
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CssCharsetRule;
            CharacterSet = newRule.CharacterSet;
            base.ReplaceWith(rule);
        }

        #endregion

        #region String Representation

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Rule("@charset", CharacterSet.CssString()));
        }

        #endregion
    }
}
