namespace AngleSharp.Css.Conditions
{
    using System;
    using System.Collections.Generic;

    sealed class GroupCondition : CssCondition
    {
        CssCondition _content;

        public CssCondition Value
        {
            get { return _content; }
            internal set { _content = value; }
        }

        protected override String Serialize()
        {
            return String.Concat("(", _content.Text, ")");
        }

        public override Boolean Check()
        {
            return _content.Check();
        }

        public override IEnumerable<CssNode> GetChildren()
        {
            if (_content != null)
                yield return _content;
        }
    }
}
