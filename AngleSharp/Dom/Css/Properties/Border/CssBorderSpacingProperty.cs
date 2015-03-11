namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-spacing
    /// </summary>
    sealed class CssBorderSpacingProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Length[]> Converter = 
            Converters.LengthConverter.Many(1, 2);

        #endregion

        #region ctor

        internal CssBorderSpacingProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderSpacing, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Zero;
        }

        protected override Object Compute(IElement element)
        {
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}
