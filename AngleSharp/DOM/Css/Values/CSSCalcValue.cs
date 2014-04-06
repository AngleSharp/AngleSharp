namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Gets a computed value. Could also be just an absolute or
    /// relative value.
    /// </summary>
    abstract class CSSCalcValue : CSSPrimitiveValue
    {
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
