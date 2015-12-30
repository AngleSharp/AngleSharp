namespace AngleSharp.Dom.Css
{
    using System;

    sealed class GroupCondition : CssNode, IConditionFunction
    {
        IConditionFunction _content;

        public IConditionFunction Content
        {
            get { return _content ?? new EmptyCondition(); }
            set
            {
                if (_content != null)
                {
                    RemoveChild(_content);
                }

                _content = value;

                if (value != null)
                {
                    AppendChild(_content);
                }
            }
        }

        public Boolean Check()
        {
            return Content.Check();
        }

        public override String ToCss(IStyleFormatter formatter)
        {
            return String.Concat("(", Content.ToCss(formatter), ")");
        }
    }
}
