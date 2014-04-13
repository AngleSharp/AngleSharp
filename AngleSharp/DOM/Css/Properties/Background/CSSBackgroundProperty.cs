namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;


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
        CSSBackgroundAttachmentProperty _attachment;
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
            _attachment = new CSSBackgroundAttachmentProperty();
            _origin = new CSSBackgroundOriginProperty();
            _clip = new CSSBackgroundClipProperty();
            _color = new CSSBackgroundColorProperty();
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            //TODO For the moment
            return true;

            if (value == CSSValue.Inherit)
                return true;
            else
            {
                var values = value as CSSValueList ?? new CSSValueList(value);
                var image = new CSSBackgroundImageProperty();
                var position = new CSSBackgroundPositionProperty();
                var size = new CSSBackgroundSizeProperty();
                var repeat = new CSSBackgroundRepeatProperty();
                var origin = new CSSBackgroundOriginProperty();
                var clip = new CSSBackgroundClipProperty();
                var color = new CSSBackgroundColorProperty();
                var list = values.ToList();

                for (int i = 0; i < list.Count; i++)
                {
                    var entry = list[i];
                    var success = false;



                    if (!success)
                        return false;
                }

                _image = image;
                _position = position;
                _repeat = repeat;
                _origin = origin;
                _size = size;
                _clip = clip;
                _color = color;
            }

            return false;
        }

        #endregion
    }
}
