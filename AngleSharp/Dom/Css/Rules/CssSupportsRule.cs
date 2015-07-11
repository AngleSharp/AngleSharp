namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Conditions;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    sealed class CssSupportsRule : CssConditionRule, ICssSupportsRule
    {
        #region Fields

        readonly CssParserOptions _options;
        ICondition _condition;

        static readonly ICondition empty = new EmptyCondition();

        #endregion

        #region ctor

        internal CssSupportsRule(CssParserOptions options)
            : base(CssRuleType.Supports, options)
        {
            _condition = empty;
            _options = options;
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
                var condition = CssParser.ParseCondition(value, _options);

                if (condition == null)
                    throw new DomException(DomError.Syntax);

                _condition = condition;
            }
        }

        /// <summary>
        /// Gets or sets the condition of the supports rule.
        /// </summary>
        public ICondition Condition
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
