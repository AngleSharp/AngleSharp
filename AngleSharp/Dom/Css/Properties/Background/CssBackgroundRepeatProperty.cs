namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-repeat
    /// Gets an enumeration with the horizontal repeat modes.
    /// Gets an enumeration with the vertical repeat modes.
    /// </summary>
    sealed class CssBackgroundRepeatProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = Converters.BackgroundRepeatsConverter.FromList().OrDefault(BackgroundRepeat.Repeat);

        #endregion

        #region ctor

        internal CssBackgroundRepeatProperty()
            : base(PropertyNames.BackgroundRepeat)
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
