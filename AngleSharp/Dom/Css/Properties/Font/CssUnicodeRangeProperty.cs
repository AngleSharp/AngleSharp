namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en/docs/Web/CSS/@font-face
    /// </summary>
    sealed class CssUnicodeRangeProperty : CssProperty
    {
        public CssUnicodeRangeProperty(CssStyleDeclaration style)
            : base(PropertyNames.UnicodeRange, style)
        {
        }

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        protected override Object Compute(IElement element)
        {
            return null;
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return true;
        }
    }
}
