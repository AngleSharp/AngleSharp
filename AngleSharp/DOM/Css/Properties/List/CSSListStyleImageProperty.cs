namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-image
    /// </summary>
    sealed class CSSListStyleImageProperty : CSSProperty, ICssListStyleImageProperty
    {
        #region Fields

        internal static readonly IImageSource Default = null;
        internal static readonly IValueConverter<IImageSource> Converter = Converters.ImageSourceConverter.Or(Keywords.None, Default);
        IImageSource _image;

        #endregion

        #region ctor

        internal CSSListStyleImageProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.ListStyleImage, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected image.
        /// </summary>
        public IImageSource Image
        {
            get { return _image; }
        }

        #endregion

        #region Methods

        public void SetImage(IImageSource image)
        {
            _image = image;
        }

        internal override void Reset()
        {
            _image = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetImage);
        }

        #endregion
    }
}
