namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-image
    /// </summary>
    sealed class CSSListStyleImageProperty : CSSProperty
    {
        #region Fields

        CSSImageValue _image;

        #endregion

        #region ctor

        public CSSListStyleImageProperty()
            : base(PropertyNames.ListStyleImage)
        {
            _inherited = true;
            _image = null;
        }

        #endregion

        #region Methods

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
