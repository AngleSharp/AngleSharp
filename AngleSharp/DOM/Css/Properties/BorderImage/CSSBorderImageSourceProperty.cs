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

        internal static readonly Url[] Default = new Url[0];
        internal static readonly IValueConverter<Url[]> Converter = TakeOne(Keywords.None, Default).Or(WithImages().To(m => m.Urls.ToArray()));
        readonly List<Url> _images;

        #endregion

        #region ctor

        internal CSSBorderImageSourceProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderImageSource, rule)
        {
            _images = new List<Url>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected image.
        /// </summary>
        public IEnumerable<Url> Images
        {
            get { return _images; }
        }

        #endregion

        #region Methods

        private void SetImages(IEnumerable<Url> urls)
        {
            _images.Clear();
            _images.AddRange(urls);
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
        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, SetImages);
        }

        #endregion
    }
}
