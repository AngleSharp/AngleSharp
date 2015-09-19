namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    sealed class CssSupportsRule : CssConditionRule, ICssSupportsRule
    {
        #region Fields

        IConditionFunction _condition;

        #endregion

        #region ctor

        internal CssSupportsRule(CssParser parser)
            : base(CssRuleType.Supports, parser)
        {
            _condition = new EmptyCondition();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the condition of the supports rule.
        /// </summary>
        public String ConditionText
        {
            get 
            {
                return _condition.ToCss(); 
            }
            set
            {
                var condition = Parser.ParseCondition(value);

                if (condition == null)
                    throw new DomException(DomError.Syntax);

                _condition = condition;
            }
        }

        /// <summary>
        /// Gets the condition of the supports rule.
        /// </summary>
        public IConditionFunction Condition
        {
            get { return _condition; }
        }

        #endregion

        #region Internal Methods

        internal void SetCondition(IConditionFunction condition)
        {
            if (condition != null)
            {
                _condition = condition;
            }
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = rule as CssSupportsRule;
            ConditionText = newRule.ConditionText;
        }

        internal override Boolean IsValid(RenderDevice device)
        {
            return _condition.Check();
        }

        #endregion

        #region String Representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            return formatter.Rule("@supports", ConditionText, rules);
        }

        #endregion
    }
}
