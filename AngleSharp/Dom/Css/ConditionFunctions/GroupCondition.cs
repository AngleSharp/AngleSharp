namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;

    sealed class GroupCondition : IConditionFunction
    {
        readonly IConditionFunction _content;

        public GroupCondition(IConditionFunction content)
        {
            _content = content;
        }

        public IEnumerable<ICssNode> Children
        {
            get { return new [] { _content }; }
        }

        public Boolean Check()
        {
            return _content.Check();
        }

        public String ToCss()
        {
            return String.Concat("(", _content.ToCss(), ")");
        }

        public String ToCss(IStyleFormatter formatter)
        {
            return ToCss();
        }
    }
}
