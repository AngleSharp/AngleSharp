namespace AngleSharp.Css.Conditions
{
    using System;

    sealed class NotCondition : CssCondition
    {
        CssCondition _content;

        public CssCondition Value 
        {
            get { return _content; }
            internal set { _content = value; }
        }
        
        protected override String Serialize()
        {
            return String.Concat("not ", _content.Text);
        }

        public override Boolean Check()
        {
            return !_content.Check();
        }
    }
}
