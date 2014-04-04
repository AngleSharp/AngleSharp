namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline
    /// </summary>
    sealed class CSSOutlineProperty : CSSProperty
    {
        #region Fields

        CSSOutlineStyleProperty _style;
        CSSOutlineWidthProperty _width;
        CSSOutlineColorProperty _color;

        #endregion

        #region ctor

        public CSSOutlineProperty()
            : base(PropertyNames.Outline)
        {
            _style = new CSSOutlineStyleProperty();
            _width = new CSSOutlineWidthProperty();
            _color = new CSSOutlineColorProperty();
            _inherited = false;
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
            var style = new CSSOutlineStyleProperty();
            var width = new CSSOutlineWidthProperty();
            var color = new CSSOutlineColorProperty();
            startGroup.Add(style);
            startGroup.Add(width);
            startGroup.Add(color);

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
                _style = style;
                _width = width;
                _color = color;
                return true;
            }

            return false;
        }

        #endregion
    }
}
