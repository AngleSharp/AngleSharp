namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style
    /// </summary>
    public sealed class CSSListStyleProperty : CSSProperty
    {
        #region Fields

        CSSListStyleTypeProperty _type;
        CSSListStyleImageProperty _image;
        CSSListStylePositionProperty _position;

        #endregion

        #region ctor

        internal CSSListStyleProperty()
            : base(PropertyNames.ListStyle)
        {
            _inherited = true;
            _type = new CSSListStyleTypeProperty();
            _image = new CSSListStyleImageProperty();
            _position = new CSSListStylePositionProperty();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected list-style type.
        /// </summary>
        public ListStyle Type
        {
            get { return _type.Style; }
        }

        /// <summary>
        /// Gets the selected image for the list.
        /// </summary>
        public CSSImageValue Image
        {
            get { return _image.Image; }
        }

        /// <summary>
        /// Gets the selected position for the list-style.
        /// </summary>
        public ListPosition Position
        {
            get { return _position.Position; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var list = value as CSSValueList ?? new CSSValueList(value);
            var index = 0;
            var startGroup = new List<CSSProperty>(3);
            var type = new CSSListStyleTypeProperty();
            var image = new CSSListStyleImageProperty();
            var position = new CSSListStylePositionProperty();
            startGroup.Add(type);
            startGroup.Add(image);
            startGroup.Add(position);

            while (true)
            {
                var length = startGroup.Count;

                for (int i = 0; i < length; i++)
                {
                    if (CheckSingleProperty(startGroup[i], index, list))
                    {
                        startGroup.RemoveAt(i);
                        index++;
                        break;
                    }
                }

                if (length == startGroup.Count)
                    break;
            }

            if (index == list.Length)
            {
                _type = type;
                _image = image;
                _position = position;
                return true;
            }

            return false;
        }

        #endregion
    }
}
