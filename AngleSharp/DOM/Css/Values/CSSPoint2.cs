using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS value pair.
    /// </summary>
    sealed class CSSPoint2
    {
        #region Members

        CSSPrimitiveValue x;
        CSSPrimitiveValue y;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the x coordinate.
        /// </summary>
        public CSSPrimitiveValue X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Gets or sets the y coordinate.
        /// </summary>
        public CSSPrimitiveValue Y
        {
            get { return y; }
            set { y = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Changes the value of X and Y to the given value.
        /// </summary>
        /// <param name="value">The value to set for all values.</param>
        public void ChangeAllTo(CSSPrimitiveValue value)
        {
            X = value;
            Y = value;
        }

        #endregion
    }
}
