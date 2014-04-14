namespace AngleSharp.DOM.Css
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// More information about the image module:
    /// http://dev.w3.org/csswg/css-images-3/
    /// </summary>
    abstract class CSSImageValue : CSSValue
    {
        #region Fields

        public static readonly CSSImageValue None = new ColorImage(Color.Transparent);

        #endregion

        #region Constructors

        public static CSSImageValue FromUrl(Location uri)
        {
            return new ImageSource(uri);
        }

        public static CSSImageValue FromUrls(IEnumerable<Location> uris)
        {
            return new ImageSources(uris);
        }

        public static CSSImageValue FromLinearGradient(Angle angle, Boolean repeating, params GradientStop[] stops)
        {
            return new LinearGradient(angle, stops, repeating);
        }

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

            Location _url;

            #endregion

            #region ctor

            public ImageSource(Location url)
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

            IEnumerable<Location> _urls;

            #endregion

            #region ctor

            public ImageSources(IEnumerable<Location> urls)
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

            CSSPrimitiveValue<Color> _color;
            CSSCalcValue _location;

            #endregion

            #region ctor

            public GradientStop(CSSPrimitiveValue<Color> color, CSSCalcValue location)
            {
                _color = color;
                _location = location;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets the color of the gradient stop.
            /// </summary>
            public CSSPrimitiveValue<Color> Color
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

            public override String ToString()
            {
                return String.Concat(_color.ToCss(), " ", _location.ToCss());
            }

            #endregion
        }

        #endregion
    }
}
