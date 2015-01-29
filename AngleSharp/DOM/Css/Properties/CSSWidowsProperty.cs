namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/widows
    /// </summary>
    sealed class CssWidowsProperty : CssProperty, ICssWidowsProperty
    {
        #region Fields

        internal static readonly Int32 Default = 2;

        /// <summary>
        /// Denotes the minimum amount of lines that can stay alone
        /// on the top of a new page. If the value is not positive,
        /// the declaration is invalid.
        /// </summary>
        Int32 _count;

        #endregion

        #region ctor

        internal CssWidowsProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Widows, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of lines, which must be left on top
        /// of a new page, on a paged media.
        /// </summary>
        public Int32 Count
        {
            get { return _count; }
        }

        #endregion

        #region Methods

        public void SetCount(Int32 value)
        {
            _count = value;
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
            return Converters.IntegerConverter.TryConvert(value, SetCount);
        }

        #endregion
    }
}
