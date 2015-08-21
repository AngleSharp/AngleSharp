namespace AngleSharp.Css
{
    using System;

    abstract class CssCondition : CssNode
    {
        public String Text
        {
            get { return Serialize(); }
        }

        public abstract Boolean Check();

        protected virtual String Serialize()
        {
            return String.Empty;
        }
    }
}
