namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius
    /// </summary>
    sealed class CSSBorderRadiusProperty : CSSShorthandProperty, ICssBorderRadiusProperty
    {
        #region Fields

        readonly CSSBorderTopLeftRadiusProperty _topLeft;
        readonly CSSBorderTopRightRadiusProperty _topRight;
        readonly CSSBorderBottomRightRadiusProperty _bottomRight;
        readonly CSSBorderBottomLeftRadiusProperty _bottomLeft;

        #endregion

        #region ctor

        internal CSSBorderRadiusProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderRadius, rule, PropertyFlags.Animatable)
        {
            _topLeft = Get<CSSBorderTopLeftRadiusProperty>();
            _topRight = Get<CSSBorderTopRightRadiusProperty>();
            _bottomRight = Get<CSSBorderBottomRightRadiusProperty>();
            _bottomLeft = Get<CSSBorderBottomLeftRadiusProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the horizontal bottom-left radius.
        /// </summary>
        public IDistance HorizontalBottomLeft
        {
            get { return _bottomLeft.HorizontalRadius; }
        }

        /// <summary>
        /// Gets the value of the vertical bottom-left radius.
        /// </summary>
        public IDistance VerticalBottomLeft
        {
            get { return _bottomLeft.VerticalRadius; }
        }

        /// <summary>
        /// Gets the value of the horizontal bottom-right radius.
        /// </summary>
        public IDistance HorizontalBottomRight
        {
            get { return _bottomRight.HorizontalRadius; }
        }

        /// <summary>
        /// Gets the value of the vertical bottom-right radius.
        /// </summary>
        public IDistance VerticalBottomRight
        {
            get { return _bottomRight.VerticalRadius; }
        }

        /// <summary>
        /// Gets the value of the horizontal top-left radius.
        /// </summary>
        public IDistance HorizontalTopLeft
        {
            get { return _topLeft.HorizontalRadius; }
        }

        /// <summary>
        /// Gets the value of the vertical top-left radius.
        /// </summary>
        public IDistance VerticalTopLeft
        {
            get { return _topLeft.VerticalRadius; }
        }

        /// <summary>
        /// Gets the value of the horizontal top-right radius.
        /// </summary>
        public IDistance HorizontalTopRight
        {
            get { return _topRight.HorizontalRadius; }
        }

        /// <summary>
        /// Gets the value of the vertical top-right radius.
        /// </summary>
        public IDistance VerticalTopRight
        {
            get { return _topRight.VerticalRadius; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="v">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue v)
        {
            var values = v as CSSValueList ?? new CSSValueList(v);
            var horizontal = new CSSValueList();
            var vertical = new CSSValueList();
            var uv = false;

            if (values.Length > 9)
                return false;

            foreach (var value in values)
            {
                if (value == CSSValue.Delimiter && uv)
                    return false;
                else if (value == CSSValue.Delimiter)
                    uv = true;
                else if (!_topLeft.CanTake(value))
                    return false;
                else if (uv)
                    vertical.Add(value);
                else
                    horizontal.Add(value);
            }

            if (!ExpandPeriodic(horizontal))
                return false;

            if (uv)
            {
                return ExpandPeriodic(vertical) && _topLeft.TrySetValue(Combine(horizontal, vertical, 0)) && _topRight.TrySetValue(Combine(horizontal, vertical, 1)) &&
                       _bottomRight.TrySetValue(Combine(horizontal, vertical, 2)) && _bottomLeft.TrySetValue(Combine(horizontal, vertical, 3));
            }

            return _topLeft.TrySetValue(horizontal[0]) && _topRight.TrySetValue(horizontal[1]) && 
                   _bottomRight.TrySetValue(horizontal[2]) && _bottomLeft.TrySetValue(horizontal[3]);
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            var horizontal = SerializePeriodic(_topLeft.HorizontalRadius, _topRight.HorizontalRadius, _bottomRight.HorizontalRadius, _bottomLeft.HorizontalRadius);

            if (_topLeft.IsCircle && _topRight.IsCircle && _bottomRight.IsCircle && _bottomLeft.IsCircle)
                return horizontal;

            var vertical = SerializePeriodic(_topLeft.VerticalRadius, _topRight.VerticalRadius, _bottomRight.VerticalRadius, _bottomLeft.VerticalRadius);
            return horizontal + " / " + vertical;
        }

        #endregion

        #region Helpers

        static CSSValueList Combine(CSSValueList h, CSSValueList v, Int32 index)
        {
            var list = new CSSValueList();
            list.Add(h[index]);
            list.Add(v[index]);
            return list;
        }

        #endregion
    }
}
