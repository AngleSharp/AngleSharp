namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-style
    /// Gets the selected decoration style.
    /// </summary>
    sealed class CssTextDecorationStyleProperty : CssProperty
    {
        #region ctor

        internal CssTextDecorationStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TextDecorationStyle, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return TextDecorationStyle.Solid;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.TextDecorationStyleConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.TextDecorationStyleConverter.Validate(value);
        }

        #endregion
    }
}
