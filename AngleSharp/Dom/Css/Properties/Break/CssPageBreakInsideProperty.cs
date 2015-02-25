namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-inside
    /// </summary>
    sealed class CssPageBreakInsideProperty : CssProperty, ICssPageBreakInsideProperty
    {
        #region Fields

        internal static readonly BreakMode Default = BreakMode.Auto;
        internal static readonly IValueConverter<BreakMode> Converter = Converters.Assign(Keywords.Auto, BreakMode.Auto).Or(Keywords.Avoid, BreakMode.Avoid);
        BreakMode _mode;

        #endregion

        #region ctor

        internal CssPageBreakInsideProperty(CssStyleDeclaration rule)
            : base(PropertyNames.PageBreakInside, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected break mode.
        /// </summary>
        public BreakMode State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetState(BreakMode mode)
        {
            _mode = mode;
        }

        internal override void Reset()
        {
            _mode = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetState);
        }

        #endregion
    }
}
