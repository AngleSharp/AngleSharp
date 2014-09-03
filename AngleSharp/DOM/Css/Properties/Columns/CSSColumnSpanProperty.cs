namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-span
    /// </summary>
    public sealed class CSSColumnSpanProperty : CSSProperty
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

        internal CSSColumnSpanProperty()
            : base(PropertyNames.ColumnSpan)
        {
            _span = false;
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
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            //The element does not span multiple columns.
            if (value.Is(Keywords.None))
                _span = false;
            //The element spans across all columns.
            else if (value.Is(Keywords.All))
                _span = true;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
