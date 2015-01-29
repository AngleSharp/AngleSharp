namespace AngleSharp.Css.Values
{
    using AngleSharp.Dom.Css;

    /// <summary>
    /// More information can be found at the W3C:
    /// http://dev.w3.org/csswg/css-images-3/#color-stop-syntax
    /// </summary>
    public struct GradientStop
    {
        #region Fields

        readonly Color _color;
        readonly Length _location;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new gradient stop.
        /// </summary>
        /// <param name="color">The color of the stop.</param>
        /// <param name="location">The location of the stop.</param>
        public GradientStop(Color color, Length location)
        {
            _color = color;
            _location = location;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the color of the gradient stop.
        /// </summary>
        public Color Color
        {
            get { return _color; }
        }

        /// <summary>
        /// Gets the location of the gradient stop.
        /// </summary>
        public Length Location
        {
            get { return _location; }
        }

        #endregion
    }
}
