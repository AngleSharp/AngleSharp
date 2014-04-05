namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-image
    /// </summary>
    sealed class CSSListStyleImageProperty : CSSProperty
    {
        #region Fields

        CSSListStyleTypeProperty _type;
        CSSListStyleImageProperty _image;
        CSSListStylePositionProperty _position;

        #endregion

        #region ctor

        public CSSListStyleImageProperty()
            : base(PropertyNames.ListStyleImage)
        {
            _inherited = true;
            _type = new CSSListStyleTypeProperty();
            _image = new CSSListStyleImageProperty();
            _position = new CSSListStylePositionProperty();
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var list = value as CSSValueList;

            if (list == null)
                list = new CSSValueList(value);

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
