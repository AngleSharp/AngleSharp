namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/max-width
    /// Gets the specified max-width of the element. A percentage is
    /// calculated with respect to the width of the containing block.
    /// </summary>
    sealed class CssMaxWidthProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Length?> Converter = 
            Converters.LengthOrPercentConverter.ToNullable().Or(Keywords.None, null);

        #endregion

        #region ctor

        internal CssMaxWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.MaxWidth, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
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
