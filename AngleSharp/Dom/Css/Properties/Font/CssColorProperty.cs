namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/color
    /// Gets the selected color for the foreground.
    /// </summary>
    sealed class CssColorProperty : CssProperty
    {
        #region ctor

        internal CssColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Color, rule, PropertyFlags.Inherited | PropertyFlags.Hashless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Color.Black;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.ColorConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.ColorConverter.Validate(value);
        }

        #endregion
    }
}
