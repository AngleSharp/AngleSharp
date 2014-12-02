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
        readonly Point _pt;
        readonly Point _size;
        readonly Boolean _repeating;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new radial gradient.
        /// </summary>
        /// <param name="pt">The center point of the gradient.</param>
        /// <param name="size">The size of the ellipsoid..</param>
        /// <param name="stops">A collection of stops to use.</param>
        /// <param name="repeating">The repeating setting.</param>
        public RadialGradient(Point pt, Point size, GradientStop[] stops, Boolean repeating = false)
        {
            _stops = stops;
            _pt = pt;
            _size = size;
            _repeating = repeating;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the x-position.
        /// </summary>
        public IDistance X
        {
            get { return _pt.X; }
        }

        /// <summary>
        /// Gets the y-position.
        /// </summary>
        public IDistance Y
        {
            get { return _pt.Y; }
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public IDistance Width
        {
            get { return _size.X; }
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public IDistance Height
        {
            get { return _size.Y; }
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
                var position = new[] { ((ICssValue)_size).CssText, Keywords.At, ((ICssValue)_pt).CssText };
                return FunctionNames.Build(_repeating ? FunctionNames.RepeatingRadialGradient : FunctionNames.RadialGradient,
                    String.Join(" ", position), String.Join(", ", _stops.Select(m => ((ICssValue)m).CssText)));
            }
        }

        #endregion
    }
}
