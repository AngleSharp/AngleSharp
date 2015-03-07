namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;
    
    /// <summary>
    /// More information can be found:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/clip
    /// </summary>
    sealed class CssClipProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Shape> Converter = Converters.ShapeConverter.OrDefault();
        internal static readonly Shape Default = null;
        Shape _shape;

        #endregion

        #region ctor

        internal CssClipProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Clip, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the shape of the selected clipping region.
        /// If this value is null, then the clipping is
        /// determined automatically.
        /// </summary>
        public Shape Clip
        {
            get { return _shape; }
        }

        #endregion

        #region Methods

        public void SetClip(Shape shape)
        {
            _shape = shape;
        }

        internal override void Reset()
        {
            _shape = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetClip);
        }

        #endregion
    }
}
