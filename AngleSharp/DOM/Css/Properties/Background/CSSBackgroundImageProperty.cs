namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
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

        readonly List<ICssObject> _images;

        #endregion

        #region ctor

        internal CSSBackgroundImageProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BackgroundImage, rule)
        {
            _images = new List<ICssObject>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration of all images.
        /// </summary>
        public IEnumerable<Object> Images
        {
            get { return _images; }
        }

        #endregion

        #region Methods

        public void SetImages(IEnumerable<ICssObject> images)
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
            return this.TakeList(
                        this.WithUrl().To(m => (ICssObject)m).Or(
                        this.WithLinearGradient().To(m => (ICssObject)m)).Or(
                        this.WithRadialGradient().To(m => (ICssObject)m))).TryConvert(value, SetImages);
        }

        #endregion
    }
}
