namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-inside
    /// or even better
    /// http://dev.w3.org/csswg/css-break/#break-inside
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssBreakInsideProperty : CssProperty
    {
        #region ctor

        internal CssBreakInsideProperty()
            : base(PropertyNames.BreakInside)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: BreakMode.Auto
            get { return Converters.BreakInsideModeConverter; }
        }

        #endregion
    }
}
