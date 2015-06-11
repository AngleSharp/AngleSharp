namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/empty-cells
    /// Gets if borders and backgrounds should be drawn like
    /// in a normal cells. Otherwise no border or backgrounds
    /// should be drawn.
    /// </summary>
    sealed class CssEmptyCellsProperty : CssProperty
    {
        #region ctor

        internal CssEmptyCellsProperty()
            : base(PropertyNames.EmptyCells, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: true
            get { return Converters.EmptyCellsConverter; }
        }

        #endregion
    }
}
