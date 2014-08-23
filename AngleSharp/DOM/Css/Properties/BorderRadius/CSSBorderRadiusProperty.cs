namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius
    /// </summary>
    public sealed class CSSBorderRadiusProperty : CSSProperty
    {
        #region Fields

        CSSCalcValue _bottomLeftHorizontal;
        CSSCalcValue _bottomRightHorizontal;
        CSSCalcValue _topLeftHorizontal;
        CSSCalcValue _topRightHorizontal;
        CSSCalcValue _bottomLeftVertical;
        CSSCalcValue _bottomRightVertical;
        CSSCalcValue _topLeftVertical;
        CSSCalcValue _topRightVertical;

        #endregion

        #region ctor

        internal CSSBorderRadiusProperty()
            : base(PropertyNames.BorderRadius)
        {
            _topRightHorizontal = CSSCalcValue.Zero;
            _bottomRightHorizontal = CSSCalcValue.Zero;
            _bottomLeftHorizontal = CSSCalcValue.Zero;
            _topLeftHorizontal = CSSCalcValue.Zero;
            _topRightVertical = CSSCalcValue.Zero;
            _bottomRightVertical = CSSCalcValue.Zero;
            _bottomLeftVertical = CSSCalcValue.Zero;
            _topLeftVertical = CSSCalcValue.Zero;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the horizontal bottom-left radius.
        /// </summary>
        internal CSSCalcValue HorizontalBottomLeft
        {
            get { return _bottomLeftHorizontal; }
        }

        /// <summary>
        /// Gets the value of the vertical bottom-left radius.
        /// </summary>
        internal CSSCalcValue VerticalBottomLeft
        {
            get { return _bottomLeftVertical; }
        }

        /// <summary>
        /// Gets the value of the horizontal bottom-right radius.
        /// </summary>
        internal CSSCalcValue HorizontalBottomRight
        {
            get { return _bottomRightHorizontal; }
        }

        /// <summary>
        /// Gets the value of the vertical bottom-right radius.
        /// </summary>
        internal CSSCalcValue VerticalBottomRight
        {
            get { return _bottomRightVertical; }
        }

        /// <summary>
        /// Gets the value of the horizontal top-left radius.
        /// </summary>
        internal CSSCalcValue HorizontalTopLeft
        {
            get { return _topLeftHorizontal; }
        }

        /// <summary>
        /// Gets the value of the vertical top-left radius.
        /// </summary>
        internal CSSCalcValue VerticalTopLeft
        {
            get { return _topLeftVertical; }
        }

        /// <summary>
        /// Gets the value of the horizontal top-right radius.
        /// </summary>
        internal CSSCalcValue HorizontalTopRight
        {
            get { return _topRightHorizontal; }
        }

        /// <summary>
        /// Gets the value of the vertical top-right radius.
        /// </summary>
        internal CSSCalcValue VerticalTopRight
        {
            get { return _topRightVertical; }
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
            else if (value is CSSValueList)
                return Check((CSSValueList)value);

            var calc = value.AsCalc();

            if (calc == null)
                return false;

            _bottomLeftHorizontal = _bottomLeftVertical = _bottomRightHorizontal = _bottomRightVertical = _topLeftHorizontal = _topLeftVertical = _topRightHorizontal = _topRightVertical = calc;
            return true;
        }

        Boolean Check(CSSValueList arguments)
        {
            var count = arguments.Length;
            var splitIndex = arguments.Length;

            for (var i = 0; i < splitIndex; i++)
                if (arguments[i] == CSSValue.Delimiter)
                    splitIndex = i;

            if (count - 1 > splitIndex + 4 || splitIndex > 4 || splitIndex == count - 1 || splitIndex == 0)
                return false;

            var values = new CSSCalcValue[4];

            for (int i = 0; i < splitIndex; i++)
            {
                values[i] = arguments[i].AsCalc();

                for (int j = 2 * i + 1; j < 4; j += i + 1)
                    values[j] = values[i];

                if (values[i] == null)
                    return false;
            }

            _topLeftHorizontal = values[0];
            _topRightHorizontal = values[1];
            _bottomRightHorizontal = values[2];
            _bottomLeftHorizontal = values[3];

            if (splitIndex != count)
            {
                splitIndex++;
                count -= splitIndex;

                for (int i = 0; i < count; i++)
                {
                    values[i] = arguments[i + splitIndex].AsCalc();

                    for (int j = 2 * i + 1; j < 4; j += i + 1)
                        values[j] = values[i];

                    if (values[i] == null)
                        return false;
                }
            }

            _topLeftVertical = values[0];
            _topRightVertical = values[1];
            _bottomRightVertical = values[2];
            _bottomLeftVertical = values[3];

            return true;
        }

        #endregion
    }
}
