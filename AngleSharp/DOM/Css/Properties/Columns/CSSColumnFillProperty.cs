namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-fill
    /// </summary>
    sealed class CSSColumnFillProperty : CSSProperty, ICssColumnFillProperty
    {
        #region Fields

        internal static readonly Boolean Default = true;
        internal static readonly IValueConverter<Boolean> Converter = Toggle(Keywords.Balance, Keywords.Auto);
        Boolean _balanced;

        #endregion

        #region ctor

        internal CSSColumnFillProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.ColumnFill, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the columns should be filled uniformly.
        /// </summary>
        public Boolean IsBalanced
        {
            get { return _balanced; }
        }

        #endregion

        #region Methods

        public void SetBalanced(Boolean balanced)
        {
            _balanced = balanced;
        }

        internal override void Reset()
        {
            _balanced = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetBalanced);
        }

        #endregion
    }
}
