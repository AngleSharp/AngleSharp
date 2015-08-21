namespace AngleSharp.Css.Conditions
{
    using System;

    sealed class GroupCondition : CssCondition
    {
        readonly CssCondition _content;

        public GroupCondition(CssCondition content)
        {
            _content = content;
        }

        protected override String Serialize()
        {
            return String.Concat("(", _content.Text, ")");
        }

        public override Boolean Check()
        {
            return _content.Check();
        }
    }
}
