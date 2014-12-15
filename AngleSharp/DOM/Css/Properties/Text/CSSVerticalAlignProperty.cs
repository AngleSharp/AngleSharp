namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/vertical-align
    /// </summary>
    sealed class CSSVerticalAlignProperty : CSSProperty, ICssVerticalAlignProperty
    {
        #region Fields

        internal static readonly VerticalAlignment Default = VerticalAlignment.Baseline;
        internal static readonly IValueConverter<VerticalAlignment> Converter = Map.VerticalAlignments.ToConverter();
        VerticalAlignment _mode;
        IDistance _shift;

        #endregion

        #region ctor

        internal CSSVerticalAlignProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.VerticalAlign, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the alignment of of the element's baseline at the given length above
        /// the baseline of its parent or like absolute values, with the percentage
        /// being a percent of the line-height property.
        /// </summary>
        public IDistance Shift
        {
            get { return _shift; }
        }

        /// <summary>
        /// Gets the selected vertical alignment mode.
        /// </summary>
        public VerticalAlignment State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetAlignment(Length shift)
        {
            _shift = shift;
            _mode = VerticalAlignment.Baseline;
        }

        public void SetAlignment(VerticalAlignment mode)
        {
            _mode = mode;
            _shift = Percent.Zero;
        }

        internal override void Reset()
        {
            _mode = Default;
            _shift = Percent.Zero;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.LengthOrPercentConverter.TryConvert(value, SetAlignment) || Converter.TryConvert(value, SetAlignment);
        }

        #endregion
    }
}
