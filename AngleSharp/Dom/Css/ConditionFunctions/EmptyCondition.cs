namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class EmptyCondition : IConditionFunction
    {
        public Boolean Check()
        {
            return true;
        }

        public IEnumerable<ICssNode> Children
        {
            get { return Enumerable.Empty<ICssNode>(); }
        }

        public String ToCss()
        {
            return String.Empty;
        }

        public String ToCss(IStyleFormatter formatter)
        {
            return ToCss();
        }
    }
}
