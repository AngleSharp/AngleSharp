namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/float
    /// Gets the value of the floating property.
    /// </summary>
    sealed class CssFloatProperty : CssProperty
    {
        #region ctor

        internal CssFloatProperty()
            : base(PropertyNames.Float)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Floating.None
            get { return Converters.FloatingConverter; }
        }

        #endregion
    }
}
