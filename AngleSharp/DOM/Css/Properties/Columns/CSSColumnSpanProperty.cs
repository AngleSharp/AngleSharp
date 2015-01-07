namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-span
    /// </summary>
    sealed class CSSColumnSpanProperty : CSSProperty, ICssColumnSpanProperty
    {
        #region Fields

        internal static readonly Boolean Default = false;
        internal static readonly IValueConverter<Boolean> Converter = Converters.Toggle(Keywords.All, Keywords.None);
        Boolean _span;

        #endregion

        #region ctor

        internal CSSColumnSpanProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ColumnSpan, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the element should span across all columns.
        /// </summary>
        public Boolean IsSpanning
        {
            get { return _span; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets if content in the normal flow that appears before the element
        /// is automatically balanced across all columns before the element appears. The
        /// element establishes a new block formatting context.
        /// </summary>
        /// <param name="span">Switches spanning on or off.</param>
        public void SetSpanning(Boolean span)
        {
            _span = span;
        }

        internal override void Reset()
        {
            _span = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetSpanning);
        }

        #endregion
    }
}
