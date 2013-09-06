using System;

namespace AngleSharp.DOM.Css
{
    sealed class CSSGradientStop
    {
        #region Members

        CSSPrimitiveValue _color;
        CSSPrimitiveValue _location;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the color of the gradient stop.
        /// </summary>
        public CSSPrimitiveValue Color
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary>
        /// Gets or sets the location of the gradient stop.
        /// </summary>
        public CSSPrimitiveValue Location
        {
            get { return _location; }
            set { _location = value; }
        }

        #endregion
    }
}
