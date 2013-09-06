using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an URI in CSS.
    /// </summary>
    sealed class CSSUri
    {
        #region Members

        String _originalUrl;
        String _url;

        #endregion

        #region ctor

        public CSSUri(String url, String basePath)
        {
            _originalUrl = url;

            if (Location.IsAbsolute(url))
                _url = url;
            else
                _url = Location.MakeAbsolute(basePath, url);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the original url.
        /// </summary>
        public String OriginalUri
        {
            get { return _originalUrl; }
        }

        /// <summary>
        /// Gets the absolute url.
        /// </summary>
        public String Uri
        {
            get { return _url; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets a (different) base path. This only affects the
        /// stored Url if it is not an absolute Url.
        /// </summary>
        /// <param name="basePath">The base URI to set.</param>
        public void SetBaseUrl(String basePath)
        {
            if (!Location.IsAbsolute(_originalUrl))
                _url = Location.MakeAbsolute(basePath, _originalUrl);
        }

        #endregion
    }
}
