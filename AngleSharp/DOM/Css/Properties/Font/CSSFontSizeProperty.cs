namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-size
    /// </summary>
    sealed class CSSFontSizeProperty : CSSProperty, ICssFontSizeProperty
    {
        #region Fields

        FontSize _mode;
        IDistance _size;

        #endregion

        #region ctor

        internal CSSFontSizeProperty()
            : base(PropertyNames.FontSize, PropertyFlags.Inherited | PropertyFlags.Unitless)
        {
            _mode = FontSize.Medium;
            _size = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the custom set font-size, if any.
        /// </summary>
        public IDistance Size
        {
            get { return _size; }
        }

        /// <summary>
        /// Gets the font-size mode.
        /// </summary>
        public FontSize Mode
        {
            get { return _mode; }
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
            FontSize? size;
            var distance = value.ToDistance();

            if (distance != null)
            {
                _size = distance;
                _mode = FontSize.Custom;
            }
            else if ((size = value.ToFontSize()).HasValue)
            {
                _size = null;
                _mode = size.Value;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
