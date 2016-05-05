namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/visibility
    /// Gets the visibility mode.
    /// </summary>
    sealed class CssVisibilityProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.VisibilityConverter.OrDefault(Visibility.Visible);

        #endregion

        #region ctor

        internal CssVisibilityProperty()
            : base(PropertyNames.Visibility, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}
