namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    sealed class CssSupportsRule : CssConditionRule, ICssSupportsRule
    {
        #region ctor

        internal CssSupportsRule(CssParser parser)
            : base(CssRuleType.Supports, parser)
        {
        }

        #endregion

        #region Properties

        public String ConditionText
        {
            get { return Condition.ToCss(); }
            set
            {
                var condition = Parser.ParseCondition(value);

                if (condition == null)
                    throw new DomException(DomError.Syntax);

                Condition = condition;
            }
        }

        public IConditionFunction Condition
        {
            get { return Children.OfType<IConditionFunction>().FirstOrDefault() ?? new EmptyCondition(); }
            set
            {
                if (value != null)
                {
                    RemoveChild(Condition);
                    AppendChild(value);
                }
            }
        }

        #endregion

        #region Internal Methods

        internal override Boolean IsValid(RenderDevice device)
        {
            return Condition.Check();
        }

        #endregion

        #region Methods

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            writer.Write(formatter.Rule("@supports", ConditionText, rules));
        }

        #endregion
    }
}
