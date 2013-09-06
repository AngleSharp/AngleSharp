using System;
using System.Collections.Generic;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS gradient() object.
    /// </summary>
    sealed class CSSGradient
    {
        #region Methods

        List<CSSGradientStop> _stops;
        CSSPrimitiveValue _angle;
        CSSPoint2 _position;
        Boolean _radial;
        Boolean _repeat;
        CSSPrimitiveValue _shape;
        CSSPrimitiveValue _size;

        #endregion

        #region ctor

        public CSSGradient(Boolean radial, Boolean repeat)
        {
            _stops = new List<CSSGradientStop>();
            _radial = radial;
            _repeat = repeat;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the radial shape.
        /// </summary>
        public CSSPrimitiveValue RadialShape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        /// <summary>
        /// Gets or sets the radial size.
        /// </summary>
        public CSSPrimitiveValue RadialSize
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// Gets or sets the horizontal radius.
        /// </summary>
        public CSSPrimitiveValue RadiusX
        {
            get { return _shape; }
            set { _shape = value; }
        }

        /// <summary>
        /// Gets or sets the vertical radius.
        /// </summary>
        public CSSPrimitiveValue RadiusY
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public CSSPoint2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        public CSSPrimitiveValue Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        /// <summary>
        /// Gets the list of gradient stops.
        /// </summary>
        public List<CSSGradientStop> Stops
        {
            get { return _stops; }
        }

        /// <summary>
        /// Gets if this gradient is radial.
        /// </summary>
        public Boolean IsRadial
        {
            get { return _radial; }
        }

        /// <summary>
        /// Gets whether this gradient is repeating.
        /// </summary>
        public Boolean IsRepeating
        {
            get { return _repeat; }
        }

        #endregion
    }
}
