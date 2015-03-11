namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-clip
    /// Gets an enumeration with the desired clip settings.
    /// </summary>
    sealed class CssBackgroundClipProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<BoxModel[]> Converter = 
            Converters.BoxModelConverter.FromList();

        #endregion

        #region ctor

        internal CssBackgroundClipProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BackgroundClip, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return BoxModel.BorderBox;
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
