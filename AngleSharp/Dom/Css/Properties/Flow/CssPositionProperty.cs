namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/position
    /// Gets the currently selected position mode.
    /// </summary>
    sealed class CssPositionProperty : CssProperty
    {
        #region ctor

        internal CssPositionProperty()
            : base(PropertyNames.Position)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: PositionMode.Static
            get { return Converters.PositionModeConverter; }
        }

        #endregion
    }
}
