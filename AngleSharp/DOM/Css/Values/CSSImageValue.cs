namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information about the image module:
    /// http://dev.w3.org/csswg/css-images-3/
    /// </summary>
    abstract class CSSImageValue : CSSValue
    {
        #region Fields

        /// <summary>
        /// Gets an image that is only a colored with a transparent color.
        /// </summary>
        public static readonly CSSImageValue None = new ColorImage(Color.Transparent);

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new image value from the given url.
        /// </summary>
        /// <param name="uri">The url pointing to an image resource.</param>
        /// <returns>A new image value.</returns>
        public static CSSImageValue FromUrl(Url uri)
        {
            return new ImageSource(uri);
        }

        /// <summary>
        /// Creates a new image value from a list of urls.
        /// </summary>
        /// <param name="uris">The list with alternative urls.</param>
        /// <returns>A new image value.</returns>
        public static CSSImageValue FromUrls(IEnumerable<Url> uris)
        {
            return new ImageSources(uris);
        }

        /// <summary>
        /// Creates a new image value from a linear gradient.
        /// </summary>
        /// <param name="angle">The angle to use.</param>
        /// <param name="repeating">The repeating setting.</param>
        /// <param name="stops">A collection of stops to use.</param>
        /// <returns>A new image value using a linear gradient.</returns>
        public static CSSImageValue FromLinearGradient(Angle angle, Boolean repeating, params GradientStop[] stops)
        {
            return new LinearGradient(angle, stops, repeating);
        }

        /// <summary>
        /// Creates a new image value from a radial gradient.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="width">The width of the ellipse.</param>
        /// <param name="height">The height of the ellipse.</param>
        /// <param name="repeating">The repeating setting.</param>
        /// <param name="stops">A collection of stops to use.</param>
        /// <returns>A new image value using a radial gradient.</returns>
        public static CSSImageValue FromRadialGradient(CSSCalcValue x, CSSCalcValue y, CSSCalcValue width, CSSCalcValue height, Boolean repeating, params GradientStop[] stops)
        {
            return new RadialLinearGradient(x, y, width, height, stops, repeating);
        }

        #endregion

        #region Specific types

        /// <summary>
        /// Represents an image with a specific color.
        /// </summary>
        sealed class ColorImage : CSSImageValue
        {
            #region Fields

            Color _color;

            #endregion

            #region ctor

            public ColorImage(Color color)
            {
                _color = color;
            }

            #endregion

            #region String Representation

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Image, _color.ToCss());
            }

            #endregion
        }

        /// <summary>
        /// http://dev.w3.org/csswg/css-images-3/#url-notation
        /// </summary>
        sealed class ImageSource : CSSImageValue
        {
            #region Fields

            readonly Url _url;

            #endregion

            #region ctor

            public ImageSource(Url url)
            {
                _url = url;
            }

            #endregion

            #region String Representation

            public override String ToCss()
            {
                return _url.ToCss();
            }

            #endregion
        }

        /// <summary>
        /// http://dev.w3.org/csswg/css-images-3/#image-notation
        /// </summary>
        sealed class ImageSources : CSSImageValue
        {
            #region Fields

            readonly IEnumerable<Url> _urls;

            #endregion

            #region ctor

            public ImageSources(IEnumerable<Url> urls)
            {
                _urls = urls;
            }

            #endregion

            #region String Representation

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Image, String.Join(", ", _urls.Select(m => m.ToCss())));
            }

            #endregion
        }

        /// <summary>
        /// http://dev.w3.org/csswg/css-images-3/#linear-gradients
        /// </summary>
        sealed class LinearGradient : CSSImageValue
        {
            #region Fields

            GradientStop[] _stops;
            Angle _angle;
            Boolean _repeating;

            #endregion

            #region ctor

            public LinearGradient(Angle angle, GradientStop[] stops, Boolean repeating)
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

            #endregion

            #region String Representation

            public override String ToCss()
            {
                return FunctionNames.Build(_repeating ? FunctionNames.RepeatingLinearGradient : FunctionNames.LinearGradient, 
                    _angle.ToCss(), String.Join(", ", _stops));
            }

            #endregion
        }

        /// <summary>
        /// http://dev.w3.org/csswg/css-images-3/#radial-gradients
        /// </summary>
        sealed class RadialLinearGradient : CSSImageValue
        {
            #region Fields

            GradientStop[] _stops;
            CSSCalcValue _x;
            CSSCalcValue _y;
            CSSCalcValue _width;
            CSSCalcValue _height;
            Boolean _repeating;

            #endregion

            #region ctor

            public RadialLinearGradient(CSSCalcValue x, CSSCalcValue y, CSSCalcValue width, CSSCalcValue height, GradientStop[] stops, Boolean repeating)
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

            #endregion

            #region String Representation

            public override String ToCss()
            {
                //TODO
                return FunctionNames.Build(_repeating ? FunctionNames.RepeatingRadialGradient : FunctionNames.RadialGradient,
                    String.Join(", ", _stops));
            }

            #endregion
        }

        #endregion

        #region Gradient Stop

        /// <summary>
        /// More information can be found at the W3C:
        /// http://dev.w3.org/csswg/css-images-3/#color-stop-syntax
        /// </summary>
        public struct GradientStop
        {
            #region Fields

            Color _color;
            CSSCalcValue _location;

            #endregion

            #region ctor

            /// <summary>
            /// Creates a new gradient stop.
            /// </summary>
            /// <param name="color">The color of the stop.</param>
            /// <param name="location">The location of the stop.</param>
            public GradientStop(Color color, CSSCalcValue location)
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
            public CSSCalcValue Location
            {
                get { return _location; }
            }

            #endregion

            #region String Representation

            /// <summary>
            /// Returns the CSS standard represenation, which is just color and location.
            /// </summary>
            /// <returns>A string that contains the color and location of the stop.</returns>
            public override String ToString()
            {
                return String.Concat(_color.ToCss(), " ", _location.ToCss());
            }

            #endregion
        }

        #endregion
    }
}
