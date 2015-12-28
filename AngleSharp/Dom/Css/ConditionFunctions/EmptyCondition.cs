namespace AngleSharp.Dom.Css
{
    using System;

    sealed class EmptyCondition : CssNode, IConditionFunction
    {
        public Boolean Check()
        {
            return true;
        }

        public override String ToCss(IStyleFormatter formatter)
        {
            return String.Empty;
        }
    }
}
