namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-right-radius
    /// </summary>
    sealed class CssBorderTopRightRadiusProperty : CssProperty
    {
        #region ctor

        internal CssBorderTopRightRadiusProperty()
            : base(PropertyNames.BorderTopRightRadius, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Length.Zero
            get { return Converters.BorderRadiusConverter; }
        }

        #endregion
    }
}
