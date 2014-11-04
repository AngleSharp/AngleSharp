namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-repeat
    /// </summary>
    sealed class CSSBorderImageRepeatProperty : CSSProperty, ICssBorderImageRepeatProperty
    {
        #region Fields

        BorderRepeat _horizontal;
        BorderRepeat _vertical;

        #endregion

        #region ctor

        internal CSSBorderImageRepeatProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderImageRepeat, rule)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the horizontal repeat value.
        /// </summary>
        public BorderRepeat Horizontal
        {
            get { return _horizontal; }
        }

        /// <summary>
        /// Gets the vertical repeat value.
        /// </summary>
        public BorderRepeat Vertical
        {
            get { return _vertical; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _horizontal = BorderRepeat.Stretch;
            _vertical = BorderRepeat.Stretch;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var mode = value.ToBorderRepeat();

            if (mode != null)
                _horizontal = _vertical = mode.Value;
            else if (value is CSSValueList)
            {
                var list = (CSSValueList)value;
                BorderRepeat? horizontal = null;
                BorderRepeat? vertical = null;

                if (list.Length > 2)
                    return false;

                foreach (var entry in list)
                {
                    mode = entry.ToBorderRepeat();

                    if (mode == null)
                        return false;
                    else if (horizontal == null)
                        horizontal = mode;
                    else
                        vertical = mode;
                }

                _horizontal = horizontal.Value;
                _vertical = vertical.Value;               
            }
            else
                return false;

            return true;
        }

        #endregion
    }
}
