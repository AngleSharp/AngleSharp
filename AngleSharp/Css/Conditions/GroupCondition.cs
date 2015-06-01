namespace AngleSharp.Css.Conditions
{
    using System;

    sealed class GroupCondition : ICondition
    {
        readonly ICondition _content;

        public GroupCondition(ICondition content)
        {
            _content = content;
        }

        public String Text
        {
            get { return String.Concat("(", _content.Text, ")"); }
        }

        public Boolean Check()
        {
            return _content.Check();
        }
    }
}
