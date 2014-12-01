namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-type
    /// </summary>
    sealed class CSSListStyleTypeProperty : CSSProperty, ICssListStyleTypeProperty
    {
        #region Fields

        internal static readonly ListStyle Default = ListStyle.Disc;
        internal static readonly IValueConverter<ListStyle> Converter = From(Map.ListStyles);
        ListStyle _style;

        #endregion

        #region ctor

        internal CSSListStyleTypeProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.ListStyleType, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected style for the list.
        /// </summary>
        public ListStyle Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        public void SetStyle(ListStyle style)
        {
            _style = style;
        }

        internal override void Reset()
        {
            _style = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetStyle);
        }

        #endregion
    }
}
