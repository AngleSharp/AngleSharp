namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en/docs/Web/CSS/@font-face
    /// </summary>
    sealed class CSSSrcProperty : CSSProperty
    {
        public CSSSrcProperty(CSSStyleDeclaration style)
            : base(PropertyNames.Src, style)
        {
        }

        internal override void Reset()
        {
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return true;
        }
    }
}
