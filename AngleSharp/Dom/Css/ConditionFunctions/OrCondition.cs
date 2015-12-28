namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class OrCondition : CssNode, IConditionFunction
    {
        readonly IConditionFunction[] _conditions;

        public OrCondition(IEnumerable<IConditionFunction> conditions)
        {
            _conditions = conditions.ToArray();
            Children = _conditions;
        }

        public Boolean Check()
        {
            foreach (var condition in _conditions)
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
            return String.Join(" or ", _conditions.Select(m => m.ToCss(formatter)));
        }
    }
}
