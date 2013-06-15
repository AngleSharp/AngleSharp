using System;

namespace AngleSharp.DOM.Css
{
    public sealed class RGBColor
    {
        #region Members

        CSSPrimitiveValue red;
        CSSPrimitiveValue blue;
        CSSPrimitiveValue green;

        #endregion

        #region ctor

        internal RGBColor()
        {
        }

        #endregion

        #region Properties

        public CSSPrimitiveValue Red
        {
            get { return red; }
            set { red = value; }
        }

        public CSSPrimitiveValue Blue
        {
            get { return blue; }
            set { blue = value; }
        }

        public CSSPrimitiveValue Green
        {
            get { return green; }
            set { green = value; }
        }

        #endregion
    }
}
