namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Gets a computed value. Could also be just an absolute or
    /// relative value.
    /// </summary>
    public abstract class CSSCalcValue : CSSValue
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

        public CSSCalcValue Add(CSSCalcValue shift)
        {
            return new ComputeAdd(this, shift);
        }

        public abstract Single ToPixel();

        #endregion

        #region Constructors

        public static CSSCalcValue FromLength(Length length)
        {
            return new Absolute(length);
        }

        public static CSSCalcValue FromPercent(Percent percent)
        {
            return new Relative(percent);
        }

        #endregion

        #region Nested

        sealed class Absolute : CSSCalcValue
        {
            Length _length;

            public Absolute(Length length)
            {
                _length = length;
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
            Percent _scale;

            public Relative(Percent scale)
            {
                _scale = scale;
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
            CSSCalcValue _origin;
            CSSCalcValue _shift;

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
