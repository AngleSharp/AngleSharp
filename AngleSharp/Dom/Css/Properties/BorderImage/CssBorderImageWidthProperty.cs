namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-width
    /// </summary>
    sealed class CssBorderImageWidthProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Length, Length, Length, Length>> StyleConverter = 
            Converters.ImageBorderWidthConverter.Periodic();

        #endregion

        #region ctor

        internal CssBorderImageWidthProperty()
            : base(PropertyNames.BorderImageWidth)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Full;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return StyleConverter.Validate(value);
        }

        #endregion
    }
}
