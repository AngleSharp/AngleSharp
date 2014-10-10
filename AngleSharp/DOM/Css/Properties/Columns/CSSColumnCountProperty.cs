namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-count
    /// </summary>
    sealed class CSSColumnCountProperty : CSSProperty, ICssColumnCountProperty
    {
        #region Fields

        /// <summary>
        /// Null indicates that other properties (column-width) should be considered.
        /// </summary>
        Int32? _count;

        #endregion

        #region ctor

        internal CSSColumnCountProperty()
            : base(PropertyNames.ColumnCount, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        public Int32? Count
        {
            get { return _count; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _count = null;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var count = value.ToInteger();

            if (count.HasValue)
                _count = count.Value;
            else if (value.Is(Keywords.Auto))
                _count = null;
            else 
                return false;

            return true;
        }

        #endregion
    }
}
