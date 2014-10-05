namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius
    /// </summary>
    sealed class CSSBorderRadiusProperty : CSSProperty, ICssBorderRadiusProperty
    {
        #region Fields

        IDistance _bottomLeftHorizontal;
        IDistance _bottomRightHorizontal;
        IDistance _topLeftHorizontal;
        IDistance _topRightHorizontal;
        IDistance _bottomLeftVertical;
        IDistance _bottomRightVertical;
        IDistance _topLeftVertical;
        IDistance _topRightVertical;

        #endregion

        #region ctor

        internal CSSBorderRadiusProperty()
            : base(PropertyNames.BorderRadius, PropertyFlags.Animatable)
        {
            _topRightHorizontal = Percent.Zero;
            _bottomRightHorizontal = Percent.Zero;
            _bottomLeftHorizontal = Percent.Zero;
            _topLeftHorizontal = Percent.Zero;
            _topRightVertical = Percent.Zero;
            _bottomRightVertical = Percent.Zero;
            _bottomLeftVertical = Percent.Zero;
            _topLeftVertical = Percent.Zero;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the horizontal bottom-left radius.
        /// </summary>
        public IDistance HorizontalBottomLeft
        {
            get { return _bottomLeftHorizontal; }
        }

        /// <summary>
        /// Gets the value of the vertical bottom-left radius.
        /// </summary>
        public IDistance VerticalBottomLeft
        {
            get { return _bottomLeftVertical; }
        }

        /// <summary>
        /// Gets the value of the horizontal bottom-right radius.
        /// </summary>
        public IDistance HorizontalBottomRight
        {
            get { return _bottomRightHorizontal; }
        }

        /// <summary>
        /// Gets the value of the vertical bottom-right radius.
        /// </summary>
        public IDistance VerticalBottomRight
        {
            get { return _bottomRightVertical; }
        }

        /// <summary>
        /// Gets the value of the horizontal top-left radius.
        /// </summary>
        public IDistance HorizontalTopLeft
        {
            get { return _topLeftHorizontal; }
        }

        /// <summary>
        /// Gets the value of the vertical top-left radius.
        /// </summary>
        public IDistance VerticalTopLeft
        {
            get { return _topLeftVertical; }
        }

        /// <summary>
        /// Gets the value of the horizontal top-right radius.
        /// </summary>
        public IDistance HorizontalTopRight
        {
            get { return _topRightHorizontal; }
        }

        /// <summary>
        /// Gets the value of the vertical top-right radius.
        /// </summary>
        public IDistance VerticalTopRight
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

            var distance = value.ToDistance();

            if (distance == null)
                return false;

            _bottomLeftHorizontal = _bottomLeftVertical = _bottomRightHorizontal = _bottomRightVertical = _topLeftHorizontal = _topLeftVertical = _topRightHorizontal = _topRightVertical = distance;
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

            var values = new IDistance[4];

            for (int i = 0; i < splitIndex; i++)
            {
                values[i] = arguments[i].ToDistance();

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
                    values[i] = arguments[i + splitIndex].ToDistance();

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
