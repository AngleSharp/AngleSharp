namespace AngleSharp.Css.Conditions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class OrCondition : ICondition
    {
        readonly ICondition[] _conditions;

        public OrCondition(IEnumerable<ICondition> conditions)
        {
            _conditions = conditions.ToArray();
        }

        public String Text
        {
            get { return String.Join(" or ", _conditions.Select(m => m.Text)); }
        }

        public Boolean Check()
        {
            foreach (var condition in _conditions)
            {
                if (condition.Check() == true)
                    return true;
            }

            return false;
        }
    }
}
