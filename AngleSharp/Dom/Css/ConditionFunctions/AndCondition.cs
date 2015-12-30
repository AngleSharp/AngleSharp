namespace AngleSharp.Dom.Css
{
    using System;
    using System.Linq;

    sealed class AndCondition : CssNode, IConditionFunction
    {
        public Boolean Check()
        {
            var conditions = Children.OfType<IConditionFunction>();

            foreach (var condition in conditions)
            {
                if (!condition.Check())
                {
                    return false;
                }
            }

            return true;
        }

        public override String ToCss(IStyleFormatter formatter)
        {
            var conditions = Children.OfType<IConditionFunction>();
            return String.Join(" and ", conditions.Select(m => m.ToCss(formatter)));
        }
    }
}
