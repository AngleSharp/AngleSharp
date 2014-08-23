namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-source
    /// </summary>
    public sealed class CSSBorderImageSourceProperty : CSSProperty
    {
        #region Fields

        CSSImageValue _image;

        #endregion

        #region ctor

        internal CSSBorderImageSourceProperty()
            : base(PropertyNames.BorderImageSource)
        {
            _image = CSSImageValue.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected image.
        /// </summary>
        internal CSSImageValue Image
        {
            get { return _image; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var image = value.AsImage();

            if (image != null)
                _image = image;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
