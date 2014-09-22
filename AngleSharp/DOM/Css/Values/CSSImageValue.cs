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

        private CSSImageValue()
            : base(CssValueType.Primitive)
        {
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

        #endregion
    }
}
