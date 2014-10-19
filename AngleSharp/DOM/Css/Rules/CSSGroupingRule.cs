namespace AngleSharp.DOM.Css
{
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents the CSSGroupingRule interface.
    /// </summary>
    abstract class CSSGroupingRule : CSSRule, ICssGroupingRule
    {
        #region Fields

        readonly CSSRuleList _rules;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS grouping rule.
        /// </summary>
        internal CSSGroupingRule()
        {
            _rules = new CSSRuleList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of all CSS rules contained within the grouping block.
        /// </summary>
        public CSSRuleList Rules
        {
            get { return _rules; }
        }

        ICssRuleList ICssGroupingRule.Rules
        {
            get { return _rules; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSGroupingRule;
            _rules.Clear();
            _rules.Import(newRule._rules, Owner, Parent);
        }

        internal override void ComputeStyle(CssPropertyBag style, IWindow window, IElement element)
        {
            _rules.ComputeStyle(style, window, element);
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
            var value = CssParser.ParseRule(rule);
            _rules.Insert(value, index, _ownerSheet, this);
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

        #endregion
    }
}
