namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-origins
    /// Gets an enumeration with the desired origin settings.
    /// </summary>
    sealed class CssBackgroundOriginProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<BoxModel[]> Converter = 
            Converters.BoxModelConverter.FromList();

        #endregion

        #region ctor

        internal CssBackgroundOriginProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BackgroundOrigin, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return BoxModel.PaddingBox;
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
