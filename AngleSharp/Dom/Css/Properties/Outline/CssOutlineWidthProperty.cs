namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-width
    /// Gets the width of the outline of an element. An outline is a line
    /// that is drawn around elements, outside the border edge, to make the
    /// element stand out.
    /// </summary>
    sealed class CssOutlineWidthProperty : CssProperty
    {
        #region ctor

        internal CssOutlineWidthProperty()
            : base(PropertyNames.OutlineWidth, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Length.Medium
            get { return Converters.LineWidthConverter; }
        }

        #endregion
    }
}
