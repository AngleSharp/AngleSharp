namespace AngleSharp.Css.Conditions
{
    using System;

    sealed class EmptyCondition : CssCondition
    {
        public override Boolean Check()
        {
            return true;
        }
    }
}
