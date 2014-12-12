namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius
    /// </summary>
    sealed class CSSBorderRadiusProperty : CSSShorthandProperty, ICssBorderRadiusProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<ICssValue, ICssValue>> Converter = Converters.WithOrder(
            CSSBorderRadiusPartProperty.SingleConverter.Periodic().Atomic().Val().Required(),
            CSSBorderRadiusPartProperty.SingleConverter.Periodic().Atomic().Val().StartsWithDelimiter().Option()
        );

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
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                _topLeft.TrySetValue(Extract(m, 0));
                _topRight.TrySetValue(Extract(m, 1));
                _bottomRight.TrySetValue(Extract(m, 2));
                _bottomLeft.TrySetValue(Extract(m, 3));
            });
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            var horizontal = SerializePeriodic(_topLeft.HorizontalRadius.CssText, _topRight.HorizontalRadius.CssText, _bottomRight.HorizontalRadius.CssText, _bottomLeft.HorizontalRadius.CssText);

            if (_topLeft.IsCircle && _topRight.IsCircle && _bottomRight.IsCircle && _bottomLeft.IsCircle)
                return horizontal;

            var vertical = SerializePeriodic(_topLeft.VerticalRadius.CssText, _topRight.VerticalRadius.CssText, _bottomRight.VerticalRadius.CssText, _bottomLeft.VerticalRadius.CssText);
            return horizontal + " / " + vertical;
        }

        #endregion

        #region Helper

        static ICssValue Extract(Tuple<ICssValue, ICssValue> src, Int32 index)
        {
            var hv = src.Item1;
            var vv = src.Item2;
            var h = hv as CssValueList;
            var v = vv as CssValueList;

            if (h != null)
                hv = Find(h, index);

            if (vv == null)
                return hv;

            var value = new CssValueList();
            value.Add(hv);

            if (v != null)
                vv = Find(v, index);

            value.Add(vv);
            return value;
        }

        static ICssValue Find(CssValueList list, Int32 index)
        {
            if (index < list.Length)
                return list[index];
            else if (index == 3 && 1 < list.Length)
                return list[1];
            
            return list[0];
        }

        #endregion
    }
}
