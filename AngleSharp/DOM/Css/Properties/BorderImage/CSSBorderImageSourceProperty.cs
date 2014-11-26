namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-source
    /// </summary>
    sealed class CSSBorderImageSourceProperty : CSSProperty, ICssBorderImageSourceProperty
    {
        #region Fields

        ICssObject _image;

        #endregion

        #region ctor

        internal CSSBorderImageSourceProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderImageSource, rule)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected image.
        /// </summary>
        public Object Image
        {
            get { return _image; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _image = Color.Transparent;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            //var image = value.ToImage();

            //if (image != null)
            //{
            //    _image = image;
            //    return true;
            //}
            
            return false;
        }

        #endregion
    }
}
