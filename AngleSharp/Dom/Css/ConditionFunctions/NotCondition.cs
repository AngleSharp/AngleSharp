namespace AngleSharp.Dom.Css
{
    using System;

    sealed class NotCondition : CssNode, IConditionFunction
    {
        readonly IConditionFunction _content;

        public NotCondition(IConditionFunction content)
        {
            _content = content;
            Children = new[] { _content };
        }

        public Boolean Check()
        {
            return !_content.Check();
        }

        public override String ToCss(IStyleFormatter formatter)
        {
            return String.Concat("not ", _content.ToCss(formatter));
        }
    }
}
