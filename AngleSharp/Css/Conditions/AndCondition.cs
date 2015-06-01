namespace AngleSharp.Css.Conditions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class AndCondition : ICondition
    {
        readonly ICondition[] _conditions;

        public AndCondition(IEnumerable<ICondition> conditions)
        {
            _conditions = conditions.ToArray();
        }

        public String Text
        {
            get { return String.Join(" and ", _conditions.Select(m => m.Text)); }
        }

        public Boolean Check()
        {
            foreach (var condition in _conditions)
            {
                if (condition.Check() == false)
                    return false;
            }

            return true;
        }
    }
}
