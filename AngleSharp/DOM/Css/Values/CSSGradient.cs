namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS gradient() object.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/gradient
    /// </summary>
    sealed class CSSGradient : ICssPrimitive
    {
        #region Fields

        List<CSSGradientStop> _stops;
        CSSUnitValue.Angle _angle;
        CSSPrimitiveValue _position;
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
        public CSSPrimitiveValue Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        public CSSUnitValue.Angle Angle
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

        #region String Representation

        /// <summary>
        /// Converts the gradient to a CSS code string.
        /// </summary>
        /// <returns>The string that represents the code.</returns>
        public String ToCss()
        {
            var sb = Pool.NewStringBuilder();

            if (_radial)
            {
                sb.Append("radial-gradient(");

                sb.Append(")");
            }
            else
            {
                sb.Append("linear-gradient(");

                for (int i = 0; i < _stops.Count; i++)
                {
                    sb.Append(_stops[i].ToCss());

                    if (i != _stops.Count - 1)
                        sb.Append(",");
                }

                sb.Append(")");
            }

            return sb.ToPool();
        }

        #endregion
    }
}
