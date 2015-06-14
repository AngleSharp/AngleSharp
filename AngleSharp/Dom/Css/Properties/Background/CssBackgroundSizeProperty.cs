namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-size
    /// </summary>
    sealed class CssBackgroundSizeProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter SingleConverter = Converters.AutoLengthOrPercentConverter.Or(
            Keywords.Cover).Or(
            Keywords.Contain).Or(
            Converters.WithOrder(
                Converters.AutoLengthOrPercentConverter.Required(), 
                Converters.AutoLengthOrPercentConverter.Required()));

        static readonly IValueConverter ListConverter = SingleConverter.FromList().OrDefault();

        #endregion

        #region ctor

        internal CssBackgroundSizeProperty()
            : base(PropertyNames.BackgroundSize, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ListConverter; }
        }

        #endregion
    }
}
