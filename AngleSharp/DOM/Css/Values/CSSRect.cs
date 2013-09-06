using System;

namespace AngleSharp.DOM.Css
{
    sealed class CSSRect
    {
        #region Members

        CSSPrimitiveValue top;
        CSSPrimitiveValue right;
        CSSPrimitiveValue bottom;
        CSSPrimitiveValue left;

        #endregion

        #region ctor

        internal CSSRect()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the top side.
        /// </summary>
        public CSSPrimitiveValue Top
        {
            get { return top; }
            set { top = value; }
        }

        /// <summary>
        /// Gets or sets the right side.
        /// </summary>
        public CSSPrimitiveValue Right
        {
            get { return right; }
            set { right = value; }
        }

        /// <summary>
        /// Gets or sets the bottom side.
        /// </summary>
        public CSSPrimitiveValue Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }

        /// <summary>
        /// Gets or sets the left side.
        /// </summary>
        public CSSPrimitiveValue Left
        {
            get { return left; }
            set { left = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the value for the given side.
        /// </summary>
        /// <param name="side">The side to use.</param>
        /// <returns>The value of the given side.</returns>
        public CSSPrimitiveValue GetSide(CssSide side)
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

        /// <summary>
        /// Sets the value for the given side.
        /// </summary>
        /// <param name="side">The side to change.</param>
        /// <param name="value">The value to apply.</param>
        public void SetSide(CssSide side, CSSPrimitiveValue value)
        {
            switch (side)
            {
                case CssSide.Top:
                    Top = value;
                    break;
                case CssSide.Right:
                    Right = value;
                    break;
                case CssSide.Bottom:
                    Bottom = value;
                    break;
                case CssSide.Left:
                    Left = value;
                    break;
            }
        }

        /// <summary>
        /// Sets the values of all sides to the given value.
        /// </summary>
        /// <param name="value">The value to apply.</param>
        public void SetAllSides(CSSPrimitiveValue value)
        {
            Top = value;
            Right = value;
            Bottom = value;
            Left = value;
        }

        #endregion
    }
}
