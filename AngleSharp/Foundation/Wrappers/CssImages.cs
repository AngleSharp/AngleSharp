namespace AngleSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Wraps a list of urls as a CSS Images value.
    /// </summary>
    sealed class CssImages : ICssObject
    {
        #region Fields

        readonly IEnumerable<Url> _urls;

        #endregion

        #region ctor

        /// <summary>
        /// Wraps the given list of image urls.
        /// </summary>
        /// <param name="urls">The urls to consider.</param>
        public CssImages(IEnumerable<Url> urls)
        {
            _urls = urls;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of urls forming the image.
        /// </summary>
        public IEnumerable<Url> Urls
        {
            get { return _urls; }
        } 

        #endregion

        #region Methods

        /// <summary>
        /// Returns the CSS standard represenation of the contained string.
        /// </summary>
        /// <returns>A string that contains the CSS code to create the value.</returns>
        public String ToCss()
        {
            return FunctionNames.Build(FunctionNames.Image, String.Join(", ", _urls.Select(m => ((ICssObject)m).ToCss())));
        }

        #endregion
    }
}
