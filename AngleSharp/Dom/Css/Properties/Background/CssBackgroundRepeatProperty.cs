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

        internal static readonly IValueConverter SingleConverter = Map.BackgroundRepeats.ToConverter().Or(
            Keywords.RepeatX).Or(
            Keywords.RepeatY).Or(
            Converters.WithOrder(
                Map.BackgroundRepeats.ToConverter().Required(), 
                Map.BackgroundRepeats.ToConverter().Required()));

        static readonly IValueConverter ListConverter = SingleConverter.FromList().OrDefault(BackgroundRepeat.Repeat);

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
