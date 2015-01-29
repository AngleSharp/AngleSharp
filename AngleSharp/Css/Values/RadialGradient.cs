namespace AngleSharp.Css.Values
{
    using AngleSharp.Dom.Css;
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
        readonly Length _width;
        readonly Length _height;
        readonly Boolean _repeating;
        readonly Boolean _circle;
        readonly SizeMode _sizeMode;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new radial gradient.
        /// </summary>
        /// <param name="circle">Determines if the radial gradient has to be forced to a circle form.</param>
        /// <param name="pt">The center point of the gradient.</param>
        /// <param name="width">The width of the ellipsoid.</param>
        /// <param name="height">The height of the ellipsoid.</param>
        /// <param name="sizeMode">The size mode of the ellipsoid.</param>
        /// <param name="stops">A collection of stops to use.</param>
        /// <param name="repeating">The repeating setting.</param>
        public RadialGradient(Boolean circle, Point pt, Length width, Length height, SizeMode sizeMode, GradientStop[] stops, Boolean repeating = false)
        {
            _stops = stops;
            _pt = pt;
            _width = width;
            _height = height;
            _repeating = repeating;
            _circle = circle;
            _sizeMode = sizeMode;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the gradient should always be displayed as a circle.
        /// </summary>
        public Boolean IsCircle
        {
            get { return _circle; }
        }

        /// <summary>
        /// Gets the special size mode of the gradient.
        /// </summary>
        public SizeMode Mode
        {
            get { return _sizeMode; }
        }

        /// <summary>
        /// Gets the x-position.
        /// </summary>
        public Length X
        {
            get { return _pt.X; }
        }

        /// <summary>
        /// Gets the y-position.
        /// </summary>
        public Length Y
        {
            get { return _pt.Y; }
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public Length Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public Length Height
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

        #region Sizes

        /// <summary>
        /// Enumeration with special size modes.
        /// </summary>
        public enum SizeMode
        {
            /// <summary>
            /// No special size mode set.
            /// </summary>
            None,
            /// <summary>
            /// Size up to the closest corner.
            /// </summary>
            ClosestCorner,
            /// <summary>
            /// Size up to the closest side.
            /// </summary>
            ClosestSide,
            /// <summary>
            /// Size up to the farthest corner.
            /// </summary>
            FarthestCorner,
            /// <summary>
            /// Size up to the farthest side.
            /// </summary>
            FarthestSide
        }

        #endregion
    }
}
