namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-count
    /// </summary>
    public sealed class CSSColumnCountProperty : CSSProperty
    {
        #region Fields

        /// <summary>
        /// Null indicates that other properties (column-width) should be considered.
        /// </summary>
        Int32? _count;

        #endregion

        #region ctor

        internal CSSColumnCountProperty()
            : base(PropertyNames.ColumnCount)
        {
            _count = null;
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the column count should be considered.
        /// </summary>
        public Boolean IsUsed
        {
            get { return _count.HasValue; }
        }

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        public Int32 Count
        {
            get { return _count.HasValue ? _count.Value : 0; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var count = value.ToInteger();

            if (count.HasValue)
                _count = count.Value;
            else if (value.Is("auto"))
                _count = null;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
