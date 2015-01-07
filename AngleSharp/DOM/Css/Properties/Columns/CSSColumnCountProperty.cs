namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-count
    /// </summary>
    sealed class CssColumnCountProperty : CssProperty, ICssColumnCountProperty
    {
        #region Fields

        internal static readonly Int32? Default = null;
        internal static readonly IValueConverter<Int32?> Converter = Converters.IntegerConverter.OrNullDefault();
        /// <summary>
        /// Null indicates that other properties (column-width) should be considered.
        /// </summary>
        Int32? _count;

        #endregion

        #region ctor

        internal CssColumnCountProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ColumnCount, rule, PropertyFlags.Animatable)
        {
            Reset();
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

        public void SetCount(Int32? count)
        {
            _count = count;
        }

        internal override void Reset()
        {
            _count = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetCount);
        }

        #endregion
    }
}
