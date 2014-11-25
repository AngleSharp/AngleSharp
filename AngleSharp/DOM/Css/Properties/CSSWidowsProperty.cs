namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/widows
    /// </summary>
    sealed class CSSWidowsProperty : CSSProperty, ICssWidowsProperty
    {
        #region Fields

        /// <summary>
        /// Denotes the minimum amount of lines that can stay alone
        /// on the top of a new page. If the value is not positive,
        /// the declaration is invalid.
        /// </summary>
        Int32 _value;

        #endregion

        #region ctor

        internal CSSWidowsProperty(CSSStyleDeclaration rule)
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
            get { return _value; }
        }

        #endregion

        #region Methods

        public void SetCount(Int32 value)
        {
            _value = value;
        }

        internal override void Reset()
        {
            _value = 2;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.WithInteger().TryConvert(value, SetCount);
        }

        #endregion
    }
}
