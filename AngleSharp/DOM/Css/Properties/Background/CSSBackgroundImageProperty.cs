namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-image
    /// </summary>
    sealed class CSSBackgroundImageProperty : CSSProperty
    {
        #region Fields

        List<Uri> _images;

        #endregion

        #region ctor

        public CSSBackgroundImageProperty()
            : base(PropertyNames.BackgroundImage)
        {
            _inherited = false;
            _images = new List<Uri>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration of all images.
        /// </summary>
        public IEnumerable<Uri> Images
        {
            get { return _images; }
        }

        #endregion

        #region Methods

        internal void AddImage(Uri image)
        {
            _images.Add(image);
        }

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals("none", StringComparison.OrdinalIgnoreCase))
                _images.Clear();
            else if (value is CSSUriValue)
            {
                _images.Clear();
                _images.Add(((CSSUriValue)value).Uri);
            }
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;
                var images = new List<Uri>();

                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] is CSSUriValue == false)
                        return false;

                    images.Add(((CSSUriValue)values[i]).Uri);

                    if (++i < values.Length && values[i] != CSSValue.Separator)
                        return false;
                }

                _images = images;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
