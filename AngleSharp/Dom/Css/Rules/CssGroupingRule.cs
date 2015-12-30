namespace AngleSharp.Dom.Css
{
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents the CSSGroupingRule interface.
    /// </summary>
    abstract class CssGroupingRule : CssRule, ICssGroupingRule
    {
        #region Fields

        readonly CssRuleList _rules;

        #endregion

        #region ctor

        internal CssGroupingRule(CssRuleType type, CssParser parser)
            : base(type, parser)
        {
            _rules = new CssRuleList(this);
        }

        #endregion

        #region Properties

        public CssRuleList Rules
        {
            get { return _rules; }
        }

        ICssRuleList ICssGroupingRule.Rules
        {
            get { return _rules; }
        }

        #endregion

        #region Methods

        public Int32 Insert(String ruleText, Int32 index)
        {
            var rule = Parser.ParseRule(ruleText);
            _rules.Insert(index, rule);
            return index;    
        }

        public void RemoveAt(Int32 index)
        {
            _rules.RemoveAt(index);
        }

        #endregion
    }
}
