namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-slice
    /// or even better:
    /// http://dev.w3.org/csswg/css-backgrounds/#border-image-slice
    /// </summary>
    public sealed class CSSBorderImageSliceProperty : CSSProperty
    {
        #region Fields

        static readonly PercentSliceMode _default = new PercentSliceMode(Percent.Hundred);
        SliceMode _top;
        SliceMode _right;
        SliceMode _bottom;
        SliceMode _left;
        Boolean _fill;

        #endregion

        #region ctor

        internal CSSBorderImageSliceProperty()
            : base(PropertyNames.BorderImageSlice)
        {
            _inherited = false;
            _top = _default;
            _right = _default;
            _bottom = _default;
            _left = _default;
            _fill = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the position of the top slicing line.
        /// </summary>
        SliceMode Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the position of the right slicing line.
        /// </summary>
        SliceMode Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the position of the bottom slicing line.
        /// </summary>
        SliceMode Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the position of the left slicing line.
        /// </summary>
        SliceMode Left
        {
            get { return _left; }
        }

        /// <summary>
        /// Gets if the center patch should be filled.
        /// </summary>
        public Boolean IsFilled
        {
            get { return _fill; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var mode = ToMode(value);

            if (mode != null)
                _top = _left = _bottom = _right = mode;
            else if (value is CSSValueList)
                return Evaluate((CSSValueList)value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        static SliceMode ToMode(CSSValue value)
        {
            var percent = value.ToPercent();

            if (percent.HasValue)
                return new PercentSliceMode(percent.Value);

            var number = value.ToNumber();

            if (number.HasValue)
                return new PixelSliceMode(number.Value);

            return null;
        }

        Boolean Evaluate(CSSValueList values)
        {
            if (values.Length > 5)
                return false;

            var fill = false;
            var modes = new List<SliceMode>(values.Length);

            foreach (var value in values)
            {
                if (!fill && value.Is("fill"))
                    fill = true;
                else if (ToMode(value) == null)
                    return false;
                else
                    modes.Add(ToMode(value));
            }

            if (modes.Count == 5 || modes.Count == 0)
                return false;

            _fill = fill;
            _top = modes[0];
            _right = modes[0];
            _left = modes[0];
            _bottom = modes[0];

            if (modes.Count > 1)
            {
                _right = modes[1];
                _left = modes[1];

                if (modes.Count > 2)
                {
                    _bottom = modes[2];

                    if (modes.Count > 3)
                        _left = modes[3];
                }
            }

            return true;
        }

        #endregion

        #region Mode

        abstract class SliceMode
        {
        }

        sealed class PixelSliceMode : SliceMode
        {
            Single _pixels;

            public PixelSliceMode(Single pixels)
            {
                _pixels = pixels;
            }
        }

        sealed class PercentSliceMode : SliceMode
        {
            Percent _percent;

            public PercentSliceMode(Percent percent)
            {
                _percent = percent;
            }
        }

        #endregion
    }
}
