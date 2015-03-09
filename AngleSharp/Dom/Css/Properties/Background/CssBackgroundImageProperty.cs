namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-image
    /// Gets the enumeration of all images.
    /// </summary>
    sealed class CssBackgroundImageProperty : CssProperty
    {
        #region Fields

        static readonly IImageSource[] Default = new IImageSource[0];
        internal static readonly IValueConverter<IImageSource[]> Converter = 
            Converters.ImageSourceConverter.FromList().Or(Keywords.None, Default);

        #endregion

        #region ctor

        internal CssBackgroundImageProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BackgroundImage, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Default;
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
