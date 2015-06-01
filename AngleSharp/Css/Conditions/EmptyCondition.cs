namespace AngleSharp.Css.Conditions
{
    using System;

    sealed class EmptyCondition : ICondition
    {
        public String Text
        {
            get { return String.Empty; }
        }

        public Boolean Check()
        {
            return true;
        }
    }
}
