namespace AngleSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a linear gradient:
    /// http://dev.w3.org/csswg/css-images-3/#linear-gradients
    /// </summary>
    public sealed class LinearGradient : ICssObject, IBitmap
    {
        #region Fields

        readonly GradientStop[] _stops;
        readonly Angle _angle;
        readonly Boolean _repeating;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new linear gradient.
        /// </summary>
        /// <param name="angle">The angle of the linear gradient.</param>
        /// <param name="stops">The stops to use.</param>
        /// <param name="repeating">Indicates if the gradient is repeating.</param>
        public LinearGradient(Angle angle, GradientStop[] stops, Boolean repeating = false)
        {
            _stops = stops;
            _angle = angle;
            _repeating = repeating;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the angle.
        /// </summary>
        public Angle Angle
        {
            get { return _angle; }
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
            return FunctionNames.Build(_repeating ? FunctionNames.RepeatingLinearGradient : FunctionNames.LinearGradient,
                _angle.ToCss(), String.Join(", ", _stops.Select(m => m.ToCss())));
        }

        #endregion
    }
}
