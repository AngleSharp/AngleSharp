namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-image
    /// </summary>
    sealed class CSSBackgroundImageProperty : CSSProperty, ICssBackgroundImageProperty
    {
        #region Fields

        internal static readonly IImageSource[] Default = new IImageSource[0];
        internal static readonly IValueConverter<IImageSource> SingleConverter = Converters.ImageSourceConverter;
        internal static readonly IValueConverter<IImageSource[]> Converter = SingleConverter.FromList().Or(Keywords.None, Default);
        readonly List<IImageSource> _images;

        #endregion

        #region ctor

        internal CSSBackgroundImageProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BackgroundImage, rule)
        {
            _images = new List<IImageSource>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration of all images.
        /// </summary>
        public IEnumerable<IImageSource> Images
        {
            get { return _images; }
        }

        #endregion

        #region Methods

        public void SetImages(IEnumerable<IImageSource> images)
        {
            _images.Clear();
            _images.AddRange(images);
        }

        internal override void Reset()
        {
            _images.Clear();
            _images.AddRange(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetImages);
        }

        #endregion
    }
}
