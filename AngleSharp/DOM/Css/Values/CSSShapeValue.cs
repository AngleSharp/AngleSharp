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

        Length top;
        Length right;
        Length bottom;
        Length left;

        #endregion

        #region ctor

        public CSSShapeValue(Length all)
            : this(all, all, all, all)
        {
        }

        public CSSShapeValue(Length y, Length x)
            : this(y, x, y, x)
        {
        }

        public CSSShapeValue(Length top, Length right, Length bottom, Length left)
        {
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.left = left;
            _text = String.Format("rect({0}, {1}, {2}, {3})", top.ToString(), right.ToString(), bottom.ToString(), left.ToString());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the top side.
        /// </summary>
        public Length Top
        {
            get { return top; }
        }

        /// <summary>
        /// Gets the right side.
        /// </summary>
        public Length Right
        {
            get { return right; }
        }

        /// <summary>
        /// Gets the bottom side.
        /// </summary>
        public Length Bottom
        {
            get { return bottom; }
        }

        /// <summary>
        /// Gets the left side.
        /// </summary>
        public Length Left
        {
            get { return left; }
        }

        #endregion
    }
}
