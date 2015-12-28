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
            _rules = new CssRuleList();
            Children = _rules;
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

        public Int32 Insert(String rule, Int32 index)
        {
            var value = Parser.ParseRule(rule);
            _rules.Insert(value, index, Owner, this);
            return index;    
        }

        public void RemoveAt(Int32 index)
        {
            _rules.RemoveAt(index);
        }

        #endregion

        #region Internal Methods

        internal void AddRule(CssRule rule)
        {
            _rules.Add(rule, Owner, this);
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (CssGroupingRule)rule;
            _rules.Clear();
            _rules.Import(newRule._rules, Owner, Parent);
        }

        #endregion
    }
}
