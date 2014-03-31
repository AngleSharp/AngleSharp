namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a CSS shape.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/shape
    /// </summary>
    sealed class CSSShapeValue : CSSPrimitiveValue
    {
        #region Fields

        CSSUnitValue.Length top;
        CSSUnitValue.Length right;
        CSSUnitValue.Length bottom;
        CSSUnitValue.Length left;

        #endregion

        #region ctor

        public CSSShapeValue(CSSUnitValue.Length all)
            : this(all, all, all, all)
        {
        }

        public CSSShapeValue(CSSUnitValue.Length y, CSSUnitValue.Length x)
            : this(y, x, y, x)
        {
        }

        public CSSShapeValue(CSSUnitValue.Length top, CSSUnitValue.Length right, CSSUnitValue.Length bottom, CSSUnitValue.Length left)
        {
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.left = left;
            _text = String.Format("rect({0}, {1}, {2}, {3})", top.ToCss(), right.ToCss(), bottom.ToCss(), left.ToCss());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the top side.
        /// </summary>
        public CSSUnitValue.Length Top
        {
            get { return top; }
        }

        /// <summary>
        /// Gets the right side.
        /// </summary>
        public CSSUnitValue.Length Right
        {
            get { return right; }
        }

        /// <summary>
        /// Gets the bottom side.
        /// </summary>
        public CSSUnitValue.Length Bottom
        {
            get { return bottom; }
        }

        /// <summary>
        /// Gets the left side.
        /// </summary>
        public CSSUnitValue.Length Left
        {
            get { return left; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the value for the given side.
        /// </summary>
        /// <param name="side">The side to use.</param>
        /// <returns>The value of the given side.</returns>
        public CSSUnitValue.Length GetSide(CssSide side)
        {
            switch (side)
            {
                case CssSide.Top:
                    return top;
                case CssSide.Right:
                    return right;
                case CssSide.Bottom:
                    return bottom;
                case CssSide.Left:
                    return left;
            }

            throw new ArgumentOutOfRangeException("Invalid value for side.");
        }

        #endregion
    }
}
