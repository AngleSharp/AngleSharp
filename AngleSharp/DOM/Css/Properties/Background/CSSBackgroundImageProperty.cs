namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-image
    /// </summary>
    sealed class CSSBackgroundImageProperty : CSSProperty, ICssBackgroundImageProperty
    {
        #region Fields

        readonly List<ICssObject> _images;

        #endregion

        #region ctor

        internal CSSBackgroundImageProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BackgroundImage, rule)
        {
            _images = new List<ICssObject>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration of all images.
        /// </summary>
        public IEnumerable<Object> Images
        {
            get { return _images; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
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
                var images = new List<ICssObject>();

                for (int i = 0; i < values.Length; i++)
                {
                    image = values[i].ToImage();

                    if (image == null || (++i < values.Length && values[i] != CSSValue.Separator))
                        return false;

                    images.Add(image);
                }

                _images.Clear();
                _images.AddRange(images);
            }
            else
                return false;

            return true;
        }

        #endregion
    }
}
