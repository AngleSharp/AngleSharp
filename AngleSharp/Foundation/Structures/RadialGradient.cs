namespace AngleSharp
{
    using AngleSharp.DOM.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a radial gradient:
    /// http://dev.w3.org/csswg/css-images-3/#radial-gradients
    /// </summary>
    sealed class RadialGradient : ICssObject
    {
        #region Fields

        readonly GradientStop[] _stops;
        readonly CSSCalcValue _x;
        readonly CSSCalcValue _y;
        readonly CSSCalcValue _width;
        readonly CSSCalcValue _height;
        readonly Boolean _repeating;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new radial gradient.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="width">The width of the ellipse.</param>
        /// <param name="height">The height of the ellipse.</param>
        /// <param name="stops">A collection of stops to use.</param>
        /// <param name="repeating">The repeating setting.</param>
        public RadialGradient(CSSCalcValue x, CSSCalcValue y, CSSCalcValue width, CSSCalcValue height, GradientStop[] stops, Boolean repeating = false)
        {
            _stops = stops;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _repeating = repeating;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the x-position.
        /// </summary>
        public CSSCalcValue X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the y-position.
        /// </summary>
        public CSSCalcValue Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public CSSCalcValue Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public CSSCalcValue Height
        {
            get { return _height; }
        }

        /// <summary>
        /// Gets an enumeration of all stops.
        /// </summary>
        public IEnumerable<GradientStop> Stops
        {
            get { return _stops.AsEnumerable(); }
        }

        /// <summary>
        /// Gets if the gradient is repeating.
        /// </summary>
        public Boolean IsRepeating
        {
            get { return _repeating; }
        } 

        #endregion

        #region String Representation

        /// <summary>
        /// Returns the CSS representation of the linear gradient function.
        /// </summary>
        /// <returns>A string that resembles CSS code.</returns>
        public String ToCss()
        {
            //TODO
            return FunctionNames.Build(_repeating ? FunctionNames.RepeatingRadialGradient : FunctionNames.RadialGradient,
                String.Join(", ", _stops.Select(m => m.ToCss())));
        }

        #endregion
    }
}
