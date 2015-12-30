namespace AngleSharp.Dom.Css
{
    using System;
    using System.Linq;

    sealed class OrCondition : CssNode, IConditionFunction
    {
        public Boolean Check()
        {
            var conditions = Children.OfType<IConditionFunction>();

            foreach (var condition in conditions)
            {
                if (condition.Check())
                {
                    return true;
                }
            }

            return false;
        }

        public override String ToCss(IStyleFormatter formatter)
        {
            var conditions = Children.OfType<IConditionFunction>();
            return String.Join(" or ", conditions.Select(m => m.ToCss(formatter)));
        }
    }
}
