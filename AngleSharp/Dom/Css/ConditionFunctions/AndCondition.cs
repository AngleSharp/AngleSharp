namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class AndCondition : CssNode, IConditionFunction
    {
        readonly IConditionFunction[] _conditions;

        public AndCondition(IEnumerable<IConditionFunction> conditions)
        {
            _conditions = conditions.ToArray();
            Children = _conditions;
        }

        public Boolean Check()
        {
            foreach (var condition in _conditions)
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
            return String.Join(" and ", _conditions.Select(m => m.ToCss(formatter)));
        }
    }
}
