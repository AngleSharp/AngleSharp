namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/widows
    /// Gets the number of lines, which must be left on top
    /// of a new page, on a paged media.
    /// </summary>
    sealed class CssWidowsProperty : CssProperty
    {
        #region ctor

        internal CssWidowsProperty()
            : base(PropertyNames.Widows, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: 2
            get { return Converters.IntegerConverter; }
        }

        #endregion
    }
}
