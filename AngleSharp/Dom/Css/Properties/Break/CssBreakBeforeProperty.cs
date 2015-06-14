namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-before
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssBreakBeforeProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.BreakModeConverter.OrDefault(BreakMode.Auto);

        #endregion

        #region ctor

        internal CssBreakBeforeProperty()
            : base(PropertyNames.BreakBefore)
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
