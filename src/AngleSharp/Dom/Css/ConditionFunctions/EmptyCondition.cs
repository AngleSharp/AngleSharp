namespace AngleSharp.Dom.Css
{
    using System;
    using System.IO;

    sealed class EmptyCondition : CssNode, IConditionFunction
    {
        public Boolean Check()
        {
            return true;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
        }
    }
}
