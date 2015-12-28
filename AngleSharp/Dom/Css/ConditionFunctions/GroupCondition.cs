namespace AngleSharp.Dom.Css
{
    using System;

    sealed class GroupCondition : CssNode, IConditionFunction
    {
        readonly IConditionFunction _content;

        public GroupCondition(IConditionFunction content)
        {
            _content = content;
            Children = new[] { _content };
        }

        public Boolean Check()
        {
            return _content.Check();
        }

        public override String ToCss(IStyleFormatter formatter)
        {
            return String.Concat("(", _content.ToCss(formatter), ")");
        }
    }
}
