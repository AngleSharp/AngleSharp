using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS value triplet.
    /// </summary>
    sealed class CSSPoint3 : ICssPrimitive
    {
        #region Members

        CSSPrimitiveValue x;
        CSSPrimitiveValue y;
        CSSPrimitiveValue z;

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

        /// <summary>
        /// Gets or sets the z coordinate.
        /// </summary>
        public CSSPrimitiveValue Z
        {
            get { return z; }
            set { z = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Changes the value of X Y and Z to the given value.
        /// </summary>
        /// <param name="value">The value to set for all values.</param>
        public void ChangeAllTo(CSSPrimitiveValue value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns the CSS representation of this object.
        /// </summary>
        /// <returns>The CSS snippet.</returns>
        public String ToCss()
        {
            return x.ToCss() + " " + y.ToCss() + " " + z.ToCss();
        }

        #endregion
    }
}
