namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform
    /// Gets the enumeration over all transformations.
    /// </summary>
    sealed class CssTransformProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.TransformConverter.Many().OrNone().OrDefault();

        #endregion

        #region ctor

        internal CssTransformProperty()
            : base(PropertyNames.Transform, PropertyFlags.Animatable)
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
