namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-image
    /// </summary>
    public sealed class CSSBackgroundImageProperty : CSSProperty
    {
        #region Fields

        List<CSSImageValue> _images;

        #endregion

        #region ctor

        internal CSSBackgroundImageProperty()
            : base(PropertyNames.BackgroundImage)
        {
            _images = new List<CSSImageValue>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration of all images.
        /// </summary>
        internal IEnumerable<CSSImageValue> Images
        {
            get { return _images; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSValueList)
            {
                var values = (CSSValueList)value;
                var images = new List<CSSImageValue>();

                for (int i = 0; i < values.Length; i++)
                {
                    var image = values[i].AsImage();

                    if (image == null || (++i < values.Length && values[i] != CSSValue.Separator))
                        return false;

                    images.Add(image);
                }

                _images = images;
            }
            else if (value != CSSValue.Inherit)
            {
                var image = value.AsImage();

                if (image == null)
                    return false;

                _images.Clear();
                _images.Add(image);
            }

            return true;
        }

        #endregion
    }
}
