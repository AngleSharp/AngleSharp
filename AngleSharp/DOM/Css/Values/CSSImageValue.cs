namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information about the image module:
    /// http://dev.w3.org/csswg/css-images-3/
    /// </summary>
    abstract class CSSImageValue : CSSPrimitiveValue
    {
        #region Fields

        public static readonly CSSImageValue None = new NoImage();

        #endregion

        #region Constructors

        public static CSSImageValue FromUrl(Uri uri)
        {
            return new ImageSource(uri);
        }

        public static CSSImageValue FromUrls(IEnumerable<Uri> uris)
        {
            return new ImageSources(uris);
        }

        public static CSSImageValue FromLinearGradient(Angle angle, Boolean repeating, params GradientStop[] stops)
        {
            return new LinearGradient(angle, stops, repeating);
        }

        public static CSSImageValue FromRadialGradient(Angle angle, Boolean repeating, params GradientStop[] stops)
        {
            return new RadialLinearGradient(angle, stops, repeating);
        }

        #endregion

        #region Specific types

        sealed class NoImage : CSSImageValue
        {
            //TODO
        }

        sealed class ImageSource : CSSImageValue
        {
            #region Fields

            Uri _url;

            #endregion

            #region ctor

            public ImageSource(Uri url)
            {
                _url = url;
            }

            #endregion
        }

        sealed class ImageSources : CSSImageValue
        {
            #region Fields

            IEnumerable<Uri> _urls;

            #endregion

            #region ctor

            public ImageSources(IEnumerable<Uri> urls)
            {
                _urls = urls;
            }

            #endregion
        }

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
        }

        sealed class RadialLinearGradient : CSSImageValue
        {
            #region Fields

            GradientStop[] _stops;
            Angle _angle;
            Boolean _repeating;

            #endregion

            #region ctor

            public RadialLinearGradient(Angle angle, GradientStop[] stops, Boolean repeating)
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

            CSSColorValue _color;
            CSSCalcValue _location;

            #endregion

            #region ctor

            public GradientStop(CSSColorValue color, CSSCalcValue location)
            {
                _color = color;
                _location = location;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets the color of the gradient stop.
            /// </summary>
            public CSSColorValue Color
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
        }

        #endregion
    }
}
