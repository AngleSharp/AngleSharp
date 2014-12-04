namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-source
    /// </summary>
    sealed class CSSBorderImageSourceProperty : CSSProperty, ICssBorderImageSourceProperty
    {
        #region Fields

        internal static readonly IImageSource Default = null;
        internal static readonly IValueConverter<IImageSource> Converter = TakeOne(Keywords.None, Default).Or(WithImageSource());
        IImageSource _image;

        #endregion

        #region ctor

        internal CSSBorderImageSourceProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderImageSource, rule)
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

        public void SetImages(IImageSource image)
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
            return Converter.TryConvert(value, SetImages);
        }

        #endregion
    }
}
