namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-width
    /// </summary>
    sealed class CSSColumnWidthProperty : CSSProperty, ICssColumnWidthProperty
    {
        #region Fields

        /// <summary>
        /// Null indicates that other properties (column-count) should be considered.
        /// </summary>
        Length? _width;

        #endregion

        #region ctor

        internal CSSColumnWidthProperty()
            : base(PropertyNames.ColumnWidth, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of a single columns.
        /// </summary>
        public Length? Width
        {
            get { return _width; }
        }

        #endregion

        #region Methods

        protected override void Reset()
        {
            _width = null;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var width = value.ToLength();

            if (width.HasValue)
                _width = width.Value;
            else if (value.Is(Keywords.Auto))
                _width = null;
            else
                return false;

            return true;
        }

        #endregion
    }
}
