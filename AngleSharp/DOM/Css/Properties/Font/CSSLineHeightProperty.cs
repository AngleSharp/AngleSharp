namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/line-height
    /// </summary>
    public sealed class CSSLineHeightProperty : CSSProperty
    {
        #region Fields

        static readonly NormalLineHeightMode _normal = new NormalLineHeightMode();
        LineHeightMode _mode;

        #endregion

        #region ctor

        public CSSLineHeightProperty()
            : base(PropertyNames.LineHeight)
        {
            _inherited = true;
            _mode = _normal;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            CSSCalcValue calc = value.ToCalc();

            if (calc != null)
                _mode = new CalcLineHeightMode(calc);
            else if (value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals("normal", StringComparison.OrdinalIgnoreCase))
                _mode = _normal;
            else if (value.ToNumber().HasValue)
                _mode = new MultipleLineHeightMode(value.ToNumber().Value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Mode

        abstract class LineHeightMode
        { }

        /// <summary>
        /// Depends on the user agent. Desktop browsers use a default value
        /// of roughly 1.2, depending on the element's font-family.
        /// </summary>
        sealed class NormalLineHeightMode : LineHeightMode
        { }

        /// <summary>
        /// The specified length is used in the calculation of the line box
        /// height. See length values for possible units.
        /// OR
        /// Relative to the font size of the element itself. The computed
        /// value is this percentage multiplied by the element's computed font size.
        /// </summary>
        sealed class CalcLineHeightMode : LineHeightMode
        {
            CSSCalcValue _calc;

            public CalcLineHeightMode(CSSCalcValue calc)
            {
                _calc = calc;
            }
        }

        /// <summary>
        /// The used value is this unitless number multiplied by the element's font size.
        /// The computed value is the same as the specified number. In most cases this is
        /// the preferred way to set line-height with no unexpected results in case of
        /// inheritance.
        /// </summary>
        sealed class MultipleLineHeightMode : LineHeightMode
        {
            Single _factor;

            public MultipleLineHeightMode(Single factor)
            {
                _factor = factor;
            }
        }

        #endregion
    }
}
