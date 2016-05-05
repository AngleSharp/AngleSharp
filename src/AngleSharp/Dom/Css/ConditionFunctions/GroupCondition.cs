namespace AngleSharp.Dom.Css
{
    using System;
    using System.IO;

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

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write("(");
            Content.ToCss(writer, formatter);
            writer.Write(")");
        }
    }
}
