namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background
    /// </summary>
    sealed class CSSBackgroundProperty : CSSProperty
    {
        #region Fields

        CSSBackgroundImageProperty _image;
        CSSBackgroundPositionProperty _position;
        CSSBackgroundSizeProperty _size;
        CSSBackgroundRepeatProperty _repeat;
        CSSBackgroundOriginProperty _origin;
        CSSBackgroundClipProperty _clip;
        CSSBackgroundColorProperty _color;

        #endregion

        #region ctor

        public CSSBackgroundProperty()
            : base(PropertyNames.Background)
        {
            _image = new CSSBackgroundImageProperty();
            _position = new CSSBackgroundPositionProperty();
            _size = new CSSBackgroundSizeProperty();
            _repeat = new CSSBackgroundRepeatProperty();
            _origin = new CSSBackgroundOriginProperty();
            _clip = new CSSBackgroundClipProperty();
            _color = new CSSBackgroundColorProperty();
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            //TODO
            //if (value != CSSValue.Inherit)
            //    return false;

            return true;
        }

        #endregion
    }
}
