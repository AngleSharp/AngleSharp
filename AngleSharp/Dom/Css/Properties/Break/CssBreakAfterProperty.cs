namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-after
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssBreakAfterProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.BreakModeConverter.OrDefault(BreakMode.Auto);

        #endregion

        #region ctor

        internal CssBreakAfterProperty()
            : base(PropertyNames.BreakAfter)
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
