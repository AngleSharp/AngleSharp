namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/backface-visibility
    /// Gets if the back face is visible, allowing the front face to be
    /// displayed mirrored. Otherwise the back face is not visible, hiding
    /// the front face.
    /// </summary>
    sealed class CssBackfaceVisibilityProperty : CssProperty
    {
        #region ctor

        internal CssBackfaceVisibilityProperty()
            : base(PropertyNames.BackfaceVisibility)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: true
            get { return Converters.BackfaceVisibilityConverter; }
        }

        #endregion
    }
}
