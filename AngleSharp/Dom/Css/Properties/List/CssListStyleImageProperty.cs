namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-image
    /// Gets the selected image.
    /// </summary>
    sealed class CssListStyleImageProperty : CssProperty
    {
        #region ctor

        internal CssListStyleImageProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ListStyleImage, rule, PropertyFlags.Inherited)
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
            return Converters.OptionalImageSourceConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.OptionalImageSourceConverter.Validate(value);
        }

        #endregion
    }
}
