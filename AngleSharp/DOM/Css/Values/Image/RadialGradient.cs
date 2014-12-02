namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a radial gradient:
    /// http://dev.w3.org/csswg/css-images-3/#radial-gradients
    /// </summary>
    sealed class RadialGradient : IImageSource
    {
        #region Fields

        readonly GradientStop[] _stops;
        readonly IDistance _x;
        readonly IDistance _y;
        readonly IDistance _width;
        readonly IDistance _height;
        readonly Boolean _repeating;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new radial gradient.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="stops">A collection of stops to use.</param>
        /// <param name="repeating">The repeating setting.</param>
        public RadialGradient(IDistance x, IDistance y, IDistance width, IDistance height, GradientStop[] stops, Boolean repeating = false)
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
        public IDistance X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the y-position.
        /// </summary>
        public IDistance Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public IDistance Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public IDistance Height
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

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get
            {
                var position = new[] { _width.CssText, _height.CssText, Keywords.At, _x.CssText, _y.CssText };
                return FunctionNames.Build(_repeating ? FunctionNames.RepeatingRadialGradient : FunctionNames.RadialGradient,
                    String.Join(" ", position), String.Join(", ", _stops.Select(m => ((ICssValue)m).CssText)));
            }
        }

        #endregion
    }
}
