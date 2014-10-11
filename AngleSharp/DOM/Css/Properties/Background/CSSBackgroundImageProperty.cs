namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-image
    /// </summary>
    sealed class CSSBackgroundImageProperty : CSSProperty, ICssBackgroundImageProperty
    {
        #region Fields

        List<IBitmap> _images;

        #endregion

        #region ctor

        internal CSSBackgroundImageProperty()
            : base(PropertyNames.BackgroundImage)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration of all images.
        /// </summary>
        public IEnumerable<IBitmap> Images
        {
            get { return _images; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            if (_images == null)
                _images = new List<IBitmap>();
            else
                _images.Clear();
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var image = value.ToImage();

            if (image != null)
            {
                _images.Clear();
                _images.Add(image);
            }
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;
                var images = new List<IBitmap>();

                for (int i = 0; i < values.Length; i++)
                {
                    image = values[i].ToImage();

                    if (image == null || (++i < values.Length && values[i] != CSSValue.Separator))
                        return false;

                    images.Add(image);
                }

                _images = images;
            }
            else
                return false;

            return true;
        }

        #endregion
    }
}
