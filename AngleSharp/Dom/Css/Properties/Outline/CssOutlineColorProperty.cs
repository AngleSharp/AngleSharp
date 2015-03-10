namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-color
    /// Gets the color of the outline.
    /// Gets if the color is inverted.
    /// </summary>
    sealed class CssOutlineColorProperty : CssProperty
    {
        #region ctor

        internal CssOutlineColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.OutlineColor, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Color.Transparent;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.InvertedColorConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.InvertedColorConverter.Validate(value);
        }

        #endregion
    }
}
