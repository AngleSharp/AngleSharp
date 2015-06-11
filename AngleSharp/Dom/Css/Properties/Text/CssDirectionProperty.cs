namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/direction
    /// Gets the selected text direction.
    /// </summary>
    sealed class CssDirectionProperty : CssProperty
    {
        #region ctor

        internal CssDirectionProperty()
            : base(PropertyNames.Direction, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: DirectionMode.Ltr
            get { return Converters.DirectionModeConverter; }
        }

        #endregion
    }
}
