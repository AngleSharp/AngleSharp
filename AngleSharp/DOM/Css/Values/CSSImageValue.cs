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
        public static readonly CSSImageValue None = new NoImage();

        public static CSSImageValue FromUrl(CSSUriValue uri)
        {
            return new ImageSource(uri.Uri);
        }

        public static CSSImageValue FromLinearGradient(CSSAngleValue angle)
        {
            return new LinearGradient(angle.Angle);
        }

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

        /// <summary>
        /// Represents a CSS gradient() object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/gradient
        /// </summary>
        sealed class LinearGradient : CSSImageValue
        {
            #region Fields

            List<GradientStop> _stops;
            Angle _angle;

            #endregion

            #region ctor

            public LinearGradient(Angle angle)
            {
                _stops = new List<GradientStop>();
                _angle = angle;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets or sets the angle.
            /// </summary>
            public Angle Angle
            {
                get { return _angle; }
                set { _angle = value; }
            }

            #endregion

            #region Gradient Stop

            /// <summary>
            /// More information can be found at the W3C:
            /// http://dev.w3.org/csswg/css-images-3/#color-stop-syntax
            /// </summary>
            struct GradientStop
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
}
