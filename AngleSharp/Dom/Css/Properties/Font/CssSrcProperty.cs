namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en/docs/Web/CSS/@font-face
    /// </summary>
    sealed class CssSrcProperty : CssProperty
    {
        public CssSrcProperty(CssStyleDeclaration style)
            : base(PropertyNames.Src, style)
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

        protected override Boolean IsValid(CssValue value)
        {
            return true;
        }
    }
}
