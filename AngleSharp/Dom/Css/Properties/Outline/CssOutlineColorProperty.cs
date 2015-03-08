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
        #region Fields

        internal static readonly IValueConverter<Color?> Converter = 
            Converters.ColorConverter.WithCurrentColor().ToNullable().Or(Keywords.Invert, null);

        #endregion

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
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}
