namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Css.Conditions;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    sealed class CssSupportsRule : CssConditionRule, ICssSupportsRule
    {
        #region Fields

        ICondition _condition;

        static readonly ICondition empty = new EmptyCondition();

        #endregion

        #region ctor

        internal CssSupportsRule()
            : base(CssRuleType.Supports)
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
                var condition = CssParser.ParseCondition(value);

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

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected override String ToCss()
        {
            return String.Concat("@supports ", ConditionText, " ", Rules.ToCssBlock());
        }

        #endregion
    }
}
