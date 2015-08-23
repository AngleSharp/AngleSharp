namespace AngleSharp.Css.Conditions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class OrCondition : CssCondition
    {
        readonly CssCondition[] _conditions;

        public OrCondition(IEnumerable<CssCondition> conditions)
        {
            _conditions = conditions.ToArray();
        }

        public override String GetSource()
        {
            var source = String.Join(Keywords.Or, _conditions.Select(m => m.GetSource()));
            return Decorate(source);
        }
        
        protected override String Serialize()
        {
            return String.Join(" or ", _conditions.Select(m => m.Text));
        }

        public override Boolean Check()
        {
            foreach (var condition in _conditions)
            {
                if (condition.Check() == true)
                    return true;
            }

            return false;
        }

        public override IEnumerable<CssNode> GetChildren()
        {
            return _conditions;
        }
    }
}
