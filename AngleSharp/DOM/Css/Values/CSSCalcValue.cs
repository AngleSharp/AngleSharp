namespace AngleSharp.DOM.Css
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Gets a computed value. Could also be just an absolute or
    /// relative value.
    /// </summary>
    abstract class CSSCalcValue : CSSPrimitiveValue
    {
        #region Fields

        /// <summary>
        /// Gets a value that computes to 50% of the original dimension.
        /// </summary>
        public static readonly CSSCalcValue Center = new Relative(0.5f);

        /// <summary>
        /// Gets a value that computes to 0.
        /// </summary>
        public static readonly CSSCalcValue Zero = new Absolute(Length.Zero);

        /// <summary>
        /// Gets a value that computes to 100% of the original dimension.
        /// </summary>
        public static readonly CSSCalcValue Full = new Relative(1f);

        #endregion

        #region Methods

        public abstract Single ToPixel();

        #endregion

        #region Constructors

        public static CSSCalcValue FromLength(Length length)
        {
            return new Absolute(length);
        }
        public static CSSCalcValue FromPercent(Single percent)
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
                _text = length.ToString();
            }

            public override Single ToPixel()
            {
                return _length.Value;
            }
        }

        sealed class Relative : CSSCalcValue
        {
            Single _scale;

            public Relative(Single scale)
            {
                _scale = scale;
                _text = (scale * 100f).ToString(CultureInfo.InvariantCulture) + "%";
            }

            public override Single ToPixel()
            {
                //TODO require some length to set the scale
                return _scale * 100f;
            }
        }

        #endregion
    }
}
