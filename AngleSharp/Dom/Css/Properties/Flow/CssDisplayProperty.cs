namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/display
    /// Gets the value of the display mode.
    /// </summary>
    sealed class CssDisplayProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.DisplayModeConverter.OrDefault(DisplayMode.Inline);

        #endregion

        #region ctor

        internal CssDisplayProperty()
            : base(PropertyNames.Display)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}
