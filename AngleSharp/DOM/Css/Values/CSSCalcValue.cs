namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Gets a computed value. Could also be just an absolute or
    /// relative value.
    /// </summary>
    abstract class CSSCalcValue : CSSValue
    {
        #region Fields

        /// <summary>
        /// Gets a value that computes to 50% of the original dimension.
        /// </summary>
        public static readonly CSSCalcValue Center = new Relative(Percent.Fifty);

        /// <summary>
        /// Gets a value that computes to 0.
        /// </summary>
        public static readonly CSSCalcValue Zero = new Absolute(Length.Zero);

        /// <summary>
        /// Gets a value that computes to 100% of the original dimension.
        /// </summary>
        public static readonly CSSCalcValue Full = new Relative(Percent.Hundred);

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new computed length value based on the current value.
        /// </summary>
        /// <param name="shift">The calculated value to add.</param>
        /// <returns>A new calc value that performs the addition.</returns>
        public CSSCalcValue Add(CSSCalcValue shift)
        {
            return new ComputeAdd(this, shift);
        }

        /// <summary>
        /// Transforms the given calculated value to pixels.
        /// </summary>
        /// <returns>A number indicating the number of pixels.</returns>
        public abstract Single ToPixel();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new calculated value from the given length.
        /// </summary>
        /// <param name="length">The absolute length to use.</param>
        /// <returns>The new calculated value.</returns>
        public static CSSCalcValue FromLength(Length length)
        {
            return new Absolute(length);
        }

        /// <summary>
        /// Creates a new calculated value from the given percentage.
        /// </summary>
        /// <param name="percent">The relative length to use.</param>
        /// <returns>The new calculated value.</returns>
        public static CSSCalcValue FromPercent(Percent percent)
        {
            return new Relative(percent);
        }

        #endregion

        #region Nested

        sealed class Absolute : CSSCalcValue
        {
            readonly Length _length;

            public Absolute(Length length)
            {
                _length = length;
            }

            public override Boolean Equals(Object obj)
            {
                var abs = obj as Absolute;

                if (abs != null)
                    return abs._length == _length;

                return false;
            }

            public override Int32 GetHashCode()
            {
                return _length.GetHashCode();
            }

            public override Single ToPixel()
            {
                return _length.ToPixel();
            }

            public override String ToCss()
            {
                return _length.ToCss();
            }
        }

        sealed class Relative : CSSCalcValue
        {
            readonly Percent _scale;

            public Relative(Percent scale)
            {
                _scale = scale;
            }

            public override Boolean Equals(Object obj)
            {
                var rel = obj as Relative;

                if (rel != null)
                    return rel._scale == _scale;

                return false;
            }

            public override Int32 GetHashCode()
            {
                return _scale.GetHashCode();
            }

            public override Single ToPixel()
            {
                //TODO require some length to set the scale
                return _scale.Value;
            }

            public override String ToCss()
            {
                return _scale.ToCss();
            }
        }

        sealed class ComputeAdd : CSSCalcValue
        {
            readonly CSSCalcValue _origin;
            readonly CSSCalcValue _shift;

            public ComputeAdd(CSSCalcValue origin, CSSCalcValue shift)
            {
                _origin = origin;
                _shift = shift;
            }

            public override Single ToPixel()
            {
                return _origin.ToPixel() + _shift.ToPixel();
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Calc, String.Concat(_origin.ToCss(), " + ", _shift.ToCss()));
            }
        }

        #endregion
    }
}
