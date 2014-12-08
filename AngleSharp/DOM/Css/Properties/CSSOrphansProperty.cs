namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/orphans
    /// </summary>
    sealed class CSSOrphansProperty : CSSProperty, ICssOrphansProperty
    {
        #region Fields

        internal static readonly IValueConverter<Int32> Converter = Converters.IntegerConverter.Constraint(m => m >= 0);
        internal static readonly Int32 Default = 2;
        Int32 _count;

        #endregion

        #region ctor

        internal CSSOrphansProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Orphans, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the minimum number of lines in a block container
        /// that must be left at the bottom of the page. 
        /// </summary>
        public Int32 Count
        {
            get { return _count; }
        }

        #endregion

        #region Methods

        public void SetCount(Int32 value)
        {
            if (value >= 0)
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
            return Converter.TryConvert(value, SetCount);
        }

        #endregion
    }
}
