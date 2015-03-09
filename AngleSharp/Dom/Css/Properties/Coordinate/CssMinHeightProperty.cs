namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-height
    /// Gets the minimum height of the element.
    /// </summary>
    sealed class CssMinHeightProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Length> Converter = 
            Converters.LengthOrPercentConverter;

        #endregion

        #region ctor

        internal CssMinHeightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.MinHeight, rule, PropertyFlags.Animatable)
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
