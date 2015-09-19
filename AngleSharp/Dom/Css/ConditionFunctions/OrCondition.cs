namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class OrCondition : IConditionFunction
    {
        readonly IConditionFunction[] _conditions;

        public OrCondition(IEnumerable<IConditionFunction> conditions)
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
                if (condition.Check())
                    return true;
            }

            return false;
        }
        
        public String ToCss()
        {
            return String.Join(" or ", _conditions.Select(m => m.ToCss()));
        }

        public string ToCss(IStyleFormatter formatter)
        {
            return ToCss();
        }
    }
}
