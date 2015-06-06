namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-image
    /// Gets the enumeration of all images.
    /// </summary>
    sealed class CssBackgroundImageProperty : CssProperty
    {
        #region ctor

        internal CssBackgroundImageProperty()
            : base(PropertyNames.BackgroundImage)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return new IImageSource[0];
        }

        protected override Object Compute(IElement element)
        {
            return Converters.MultipleImageSourceConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.MultipleImageSourceConverter.Validate(value);
        }

        #endregion
    }
}
