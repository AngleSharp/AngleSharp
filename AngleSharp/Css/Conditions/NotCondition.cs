namespace AngleSharp.Css.Conditions
{
    using System;

    sealed class NotCondition : CssCondition
    {
        readonly CssCondition _content;

        public NotCondition(CssCondition content)
        {
            _content = content;
        }

        public CssCondition Value 
        {
            get { return _content; }
        }
        
        public override String ToCss()
        {
            return String.Concat("not ", _content.ToCss());
        }

        public override Boolean Check()
        {
            return !_content.Check();
        }
    }
}
