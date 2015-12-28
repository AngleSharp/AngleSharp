namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;

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
            Children = GetChildren();
        }

        #endregion

        #region Properties

        public String ConditionText
        {
            get { return _condition.ToCss(); }
            set
            {
                var condition = Parser.ParseCondition(value);

                if (condition == null)
                {
                    throw new DomException(DomError.Syntax);
                }

                _condition = condition;
            }
        }

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

        internal override Boolean IsValid(RenderDevice device)
        {
            return _condition.Check();
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = rule as CssSupportsRule;
            ConditionText = newRule.ConditionText;
        }

        IEnumerable<ICssNode> GetChildren()
        {
            yield return _condition;
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
