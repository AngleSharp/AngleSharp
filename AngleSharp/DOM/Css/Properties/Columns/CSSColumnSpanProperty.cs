namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-span
    /// </summary>
    sealed class CSSColumnSpanProperty : CSSProperty, ICssColumnSpanProperty
    {
        #region Fields

        /// <summary>
        /// Content in the normal flow that appears before the element is automatically
        /// balanced across all columns before the element appears. The element
        /// establishes a new block formatting context.
        /// </summary>
        Boolean _span;

        #endregion

        #region ctor

        internal CSSColumnSpanProperty(CSSStyleDeclaration rule)
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

        public void SetSpanning(Boolean span)
        {
            _span = span;
        }

        internal override void Reset()
        {
            _span = false;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return Toggle(Keywords.All, Keywords.None).TryConvert(value, SetSpanning);
        }

        #endregion
    }
}
