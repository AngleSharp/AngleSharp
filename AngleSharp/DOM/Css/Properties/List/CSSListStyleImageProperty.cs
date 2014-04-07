namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-image
    /// </summary>
    sealed class CSSListStyleImageProperty : CSSProperty
    {
        #region Fields

        static readonly DefaultImageMode _default = new DefaultImageMode();
        ImageMode _mode;

        #endregion

        #region ctor

        public CSSListStyleImageProperty()
            : base(PropertyNames.ListStyleImage)
        {
            _inherited = true;
            _mode = _default;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals("none", StringComparison.OrdinalIgnoreCase))
                _mode = _default;
            else if (value is CSSUriValue)
                _mode = new CustomImageMode(((CSSUriValue)value).Uri);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class ImageMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// Default value.
        /// </summary>
        sealed class DefaultImageMode : ImageMode
        {
        }

        /// <summary>
        /// Location of image to use as the marker.
        /// </summary>
        sealed class CustomImageMode : ImageMode
        {
            Uri _url;

            public CustomImageMode(Uri url)
            {
                _url = url;
            }
        }

        #endregion
    }
}
