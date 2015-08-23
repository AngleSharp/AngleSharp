namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Conditions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    sealed class CssSupportsRule : CssConditionRule, ICssSupportsRule
    {
        #region Fields

        CssCondition _condition;

        static readonly CssCondition empty = new EmptyCondition();

        #endregion

        #region ctor

        internal CssSupportsRule(CssParser parser)
            : base(CssRuleType.Supports, parser)
        {
            _condition = empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the condition of the supports rule.
        /// </summary>
        public String ConditionText
        {
            get { return _condition.Text; }
            set
            {
                var condition = Parser.ParseCondition(value);

                if (condition == null)
                    throw new DomException(DomError.Syntax);

                _condition = condition;
            }
        }

        /// <summary>
        /// Gets or sets the condition of the supports rule.
        /// </summary>
        public CssCondition Condition
        {
            get { return _condition; }
            set { _condition = value ?? empty; }
        }

        /// <summary>
        /// Gets if the rule is used, i.e. if the condition is fulfilled.
        /// </summary>
        public Boolean IsSupported
        {
            get { return _condition.Check(); }
        }

        #endregion

        #region Methods

        public override String GetSource()
        {
            var rules = base.GetSource();
            var source = String.Concat("@supports", _condition.GetSource(), rules);
            return Decorate(source);
        }

        public override IEnumerable<CssNode> GetChildren()
        {
            if (_condition != null)
                yield return _condition;

            foreach (var child in base.GetChildren())
                yield return child;
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = rule as CssSupportsRule;
            ConditionText = newRule.ConditionText;
        }

        internal override Boolean IsValid(RenderDevice device)
        {
            return true;
        }

        #endregion

        #region String representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            return formatter.Rule("@supports", ConditionText, rules);
        }

        #endregion
    }
}
