namespace AngleSharp.Css
{
    using System;

    abstract class CssCondition : IStyleFormattable
    {
        public abstract Boolean Check();

        public virtual String ToCss()
        {
            return String.Empty;
        }

        public String ToCss(IStyleFormatter formatter)
        {
            return ToCss();
        }
    }
}
