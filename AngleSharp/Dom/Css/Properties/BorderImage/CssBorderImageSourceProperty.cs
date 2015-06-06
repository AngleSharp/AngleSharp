namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-source
    /// </summary>
    sealed class CssBorderImageSourceProperty : CssProperty
    {
        #region ctor

        internal CssBorderImageSourceProperty()
            : base(PropertyNames.BorderImageSource)
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

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.OptionalImageSourceConverter.Validate(value);
        }

        #endregion
    }
}
