namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class AndCondition : IConditionFunction
    {
        readonly IConditionFunction[] _conditions;

        public AndCondition(IEnumerable<IConditionFunction> conditions)
        {
            _conditions = conditions.ToArray();
        }

        public IEnumerable<ICssNode> Children
        {
            get { return _conditions; }
        }

        public Boolean Check()
        {
            foreach (var condition in _conditions)
            {
                if (!condition.Check())
                    return false;
            }

            return true;
        }

        public String ToCss(IStyleFormatter formatter)
        {
            return String.Join(" and ", _conditions.Select(m => m.ToCss(formatter)));
        }
    }
}
