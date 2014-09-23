namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-width
    /// </summary>
    sealed class CSSBorderImageWidthProperty : CSSProperty, ICssBorderImageWidthProperty
    {
        #region Fields

        static readonly MultipleImageWidthMode _default = new MultipleImageWidthMode(1f);
        static readonly AutoImageWidthMode _auto = new AutoImageWidthMode();

        ImageWidthMode _top;
        ImageWidthMode _right;
        ImageWidthMode _bottom;
        ImageWidthMode _left;

        #endregion

        #region ctor

        internal CSSBorderImageWidthProperty()
            : base(PropertyNames.BorderImageWidth)
        {
            _top = _default;
            _right = _default;
            _bottom = _default;
            _left = _default;
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var mode = ToMode(value);

            if (mode != null)
                _top = _right = _left = _bottom = mode;
            else if (value is CSSValueList)
                return Evaluate((CSSValueList)value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        static ImageWidthMode ToMode(CSSValue value)
        {
            if (value.Is(Keywords.Auto))
                return _auto;

            var multiple = value.ToSingle();

            if (multiple.HasValue)
                return new MultipleImageWidthMode(multiple.Value);

            var distance = value.ToDistance();

            if (distance != null)
                return new CalcImageWidthMode(distance);

            return null;
        }

        Boolean Evaluate(CSSValueList values)
        {
            if (values.Length > 4)
                return false;

            var top = ToMode(values[0]);
            var right = ToMode(values[1]);
            var bottom = top;
            var left = right;

            if (top == null || right == null)
                return false;

            if (values.Length > 2)
            {
                bottom = ToMode(values[2]);

                if (bottom == null)
                    return false;

                if (values.Length > 3)
                {
                    left = ToMode(values[3]);

                    if (left == null)
                        return false;
                }
            }

            _left = left;
            _right = right;
            _bottom = bottom;
            _top = top;
            return true;
        }

        #endregion

        #region Mode

        abstract class ImageWidthMode
        { }

        /// <summary>
        /// Indicates that the width, or height, of the image size must be the
        /// intrinsic size of that dimension.
        /// </summary>
        sealed class AutoImageWidthMode : ImageWidthMode
        { }

        /// <summary>
        /// Represents the length of the image slice. It can be an absolute or
        /// relative length. This length must not be negative.
        /// OR
        /// Represents the percentage of the image slice relative to the height,
        /// or width, of the border image area. The percentage must not be negative.
        /// </summary>
        sealed class CalcImageWidthMode : ImageWidthMode
        {
            readonly IDistance _calc;

            public CalcImageWidthMode(IDistance calc)
            {
                _calc = calc;
            }
        }

        /// <summary>
        /// Represents a multiple of the computed value of the element's border-width
        /// property to be used as the image slice size. The number must not be negative.
        /// </summary>
        sealed class MultipleImageWidthMode : ImageWidthMode
        {
            Single _factor;

            public MultipleImageWidthMode(Single factor)
            {
                _factor = factor;
            }
        }

        #endregion
    }
}
