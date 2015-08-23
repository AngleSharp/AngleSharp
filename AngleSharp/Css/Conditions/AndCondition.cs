namespace AngleSharp.Css.Conditions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class AndCondition : CssCondition
    {
        readonly CssCondition[] _conditions;

        public AndCondition(IEnumerable<CssCondition> conditions)
        {
            _conditions = conditions.ToArray();
        }

        public override String GetSource()
        {
            var source = String.Join(Keywords.And, _conditions.Select(m => m.GetSource()));
            return Decorate(source);
        }

        protected override String Serialize()
        {
            return String.Join(" and ", _conditions.Select(m => m.Text));
        }

        public override Boolean Check()
        {
            foreach (var condition in _conditions)
            {
                if (condition.Check() == false)
                    return false;
            }

            return true;
        }

        public override IEnumerable<CssNode> GetChildren()
        {
            return _conditions;
        }
    }
}
