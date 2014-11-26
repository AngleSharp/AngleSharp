namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-image
    /// </summary>
    sealed class CSSListStyleImageProperty : CSSProperty, ICssListStyleImageProperty
    {
        #region Fields

        readonly List<Url> _images;

        #endregion

        #region ctor

        internal CSSListStyleImageProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.ListStyleImage, rule, PropertyFlags.Inherited)
        {
            _images = new List<Url>();
            Reset();
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

        public void SetImages(IEnumerable<Url> images)
        {
            _images.Clear();
            _images.AddRange(images);
        }

        internal override void Reset()
        {
            _images.Clear();
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.WithImages().To(m => m.Urls).TryConvert(value, SetImages);
        }

        #endregion
    }
}
