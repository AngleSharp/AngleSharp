using System;

namespace AngleSharp.DOM.Css
{
    sealed class Rect
    {
        #region Members

        CSSPrimitiveValue top;
        CSSPrimitiveValue right;
        CSSPrimitiveValue bottom;
        CSSPrimitiveValue left;

        #endregion

        #region ctor

        internal Rect()
        {
        }

        #endregion

        #region Properties

        public CSSPrimitiveValue Top
        {
            get { return top; }
            set { top = value; }
        }

        public CSSPrimitiveValue Right
        {
            get { return right; }
            set { right = value; }
        }

        public CSSPrimitiveValue Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }

        public CSSPrimitiveValue Left
        {
            get { return left; }
            set { left = value; }
        }

        #endregion
    }
}
