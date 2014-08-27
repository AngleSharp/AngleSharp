namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline
    /// </summary>
    sealed class CSSOutlineProperty : CSSProperty, ICssOutlineProperty
    {
        #region Fields

        CSSOutlineStyleProperty _style;
        CSSOutlineWidthProperty _width;
        CSSOutlineColorProperty _color;

        #endregion

        #region ctor

        internal CSSOutlineProperty()
            : base(PropertyNames.Outline)
        {
            _style = new CSSOutlineStyleProperty();
            _width = new CSSOutlineWidthProperty();
            _color = new CSSOutlineColorProperty();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected outline style property.
        /// </summary>
        public CSSOutlineStyleProperty Style
        {
            get { return _style; }
        }

        /// <summary>
        /// Gets the selected outline width property.
        /// </summary>
        public CSSOutlineWidthProperty Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the selected outline color property.
        /// </summary>
        public CSSOutlineColorProperty Color
        {
            get { return _color; }
        }

        Color ICssOutlineColorProperty.Color
        {
            get { return _color.Color; }
        }

        LineStyle ICssOutlineStyleProperty.Style
        {
            get { return _style.Style; }
        }

        Length ICssOutlineWidthProperty.Width
        {
            get { return _width.Width; }
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
