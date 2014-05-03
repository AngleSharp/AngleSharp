namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a medium rule.
    /// </summary>
    sealed class CSSMedium : ICssObject
    {
        public Boolean IsOnly
        {
            get;
            set;
        }

        public abstract String ToCss();
    }
}
