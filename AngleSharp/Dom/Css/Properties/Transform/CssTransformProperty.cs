namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform
    /// Gets the enumeration over all transformations.
    /// </summary>
    sealed class CssTransformProperty : CssProperty
    {
        #region Fields

        static readonly ITransform[] Default = new ITransform[0];
        static readonly IValueConverter<ITransform[]> StyleConverter = 
            Converters.TransformConverter.Many().Or(Keywords.None, Default);

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
            // Default: Default
            get { return StyleConverter; }
        }

        #endregion
    }
}
