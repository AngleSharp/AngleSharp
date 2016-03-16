namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
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
            var content = Content.ToCss(formatter);
            return String.Empty.CssFunction(content);
        }
    }
}
