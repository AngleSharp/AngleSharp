namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSSGroupingRule interface.
    /// </summary>
    abstract class CssGroupingRule : CssRule, ICssGroupingRule
    {
        #region Fields

        readonly CssRuleList _rules;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS grouping rule.
        /// </summary>
        internal CssGroupingRule(CssRuleType type, CssParser parser)
            : base(type, parser)
        {
            _rules = new CssRuleList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of all CSS rules contained within the grouping block.
        /// </summary>
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

        /// <summary>
        /// Used to insert a new rule into the media block.
        /// </summary>
        /// <param name="rule">
        /// The parsable text representing the rule. For rule sets this contains
        /// both the selector and the style declaration. For at-rules, this
        /// specifies both the at-identifier and the rule content.
        /// </param>
        /// <param name="index">
        /// The index within the media block's rule collection of the rule before
        /// which to insert the specified rule.
        /// </param>
        /// <returns>
        /// The index within the media block's rule collection of the newly
        /// inserted rule.
        /// </returns>
        public Int32 Insert(String rule, Int32 index)
        {
            var value = Parser.ParseRule(rule);
            _rules.Insert(value, index, Owner, this);
            return index;    
        }

        /// <summary>
        /// Used to delete a rule from the media block.
        /// </summary>
        /// <param name="index">
        /// The index within the media block's rule collection of the rule to remove.
        /// </param>
        public void RemoveAt(Int32 index)
        {
            _rules.RemoveAt(index);
        }

        public override String GetSource()
        {
            var rules = new String[_rules.Length];

            for (int i = 0; i < rules.Length; i++)
                rules[i] = _rules[i].GetSource();

            return String.Concat("{", String.Join(String.Empty, rules), "}");
        }

        public override IEnumerable<CssNode> GetChildren()
        {
            for (var i = 0; i < _rules.Length; i++)
                yield return _rules[i];
        }

        #endregion

        #region Internal Methods

        internal void AddRule(CssRule rule)
        {
            if (rule != null)
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
