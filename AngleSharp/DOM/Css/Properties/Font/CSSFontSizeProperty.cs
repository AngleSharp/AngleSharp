namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
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

        internal CSSFontSizeProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.FontSize, rule, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
            Reset();
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
        public FontSize SizingMode
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _mode = FontSize.Medium;
            _size = _mode.ToDistance();
        }

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
                var mode = size.Value;
                _size = mode.ToDistance();
                _mode = mode;
            }
            else
                return false;

            return true;
        }

        #endregion
    }
}
