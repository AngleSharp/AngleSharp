namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-image
    /// </summary>
    sealed class CSSListStyleImageProperty : CSSProperty, ICssListStyleImageProperty
    {
        #region Fields

        IBitmap _image;

        #endregion

        #region ctor

        internal CSSListStyleImageProperty()
            : base(PropertyNames.ListStyleImage, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected image.
        /// </summary>
        public IBitmap Image
        {
            get { return _image; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _image = null;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var image = value.ToImage();

            if (image != null)
            {
                _image = image;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
