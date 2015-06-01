namespace AngleSharp.Css.Conditions
{
    using System;

    sealed class NotCondition : ICondition
    {
        readonly ICondition _content;

        public NotCondition(ICondition content)
        {
            _content = content;
        }

        public String Text
        {
            get { return String.Concat("not ", _content.Text); }
        }

        public Boolean Check()
        {
            return !_content.Check();
        }
    }
}
