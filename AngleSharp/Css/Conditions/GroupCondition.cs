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

        public CssCondition Value
        {
            get { return _content; }
        }

        public override String ToCss()
        {
            return String.Concat("(", _content.ToCss(), ")");
        }

        public override Boolean Check()
        {
            return _content.Check();
        }
    }
}
