namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-type
    /// Gets the selected style for the list.
    /// </summary>
    sealed class CssListStyleTypeProperty : CssProperty
    {
        #region ctor

        internal CssListStyleTypeProperty()
            : base(PropertyNames.ListStyleType, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: ListStyle.Disc
            get { return Converters.ListStyleConverter; }
        }

        #endregion
    }
}
