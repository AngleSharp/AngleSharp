namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/vertical-align
    /// </summary>
    public sealed class CSSVerticalAlignProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, VerticalAlignment> modes = new Dictionary<String, VerticalAlignment>(StringComparer.OrdinalIgnoreCase);
        VerticalAlignment _mode;
        CSSCalcValue _shift;

        #endregion

        #region ctor

        static CSSVerticalAlignProperty()
        {
            modes.Add("baseline", VerticalAlignment.Baseline);
            modes.Add("sub", VerticalAlignment.Sub);
            modes.Add("super", VerticalAlignment.Super);
            modes.Add("text-top", VerticalAlignment.TextTop);
            modes.Add("text-bottom", VerticalAlignment.TextBottom);
            modes.Add("middle", VerticalAlignment.Middle);
            modes.Add("top", VerticalAlignment.Top);
            modes.Add("bottom", VerticalAlignment.Bottom);
        }

        internal CSSVerticalAlignProperty()
            : base(PropertyNames.VerticalAlign)
        {
            _inherited = false;
            _mode = VerticalAlignment.Baseline;
            _shift = CSSCalcValue.Zero;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the alignment of of the element's baseline at the given length above
        /// the baseline of its parent or like absolute values, with the percentage
        /// being a percent of the line-height property.
        /// </summary>
        internal CSSCalcValue Shift
        {
            get { return _shift; }
        }

        /// <summary>
        /// Gets the selected vertical alignment mode.
        /// </summary>
        public VerticalAlignment Align
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            VerticalAlignment mode;
            var calc = value.AsCalc();

            if (calc != null)
            {
                _shift = calc;
                _mode = VerticalAlignment.Baseline;
            }
            else if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
            {
                _shift = CSSCalcValue.Zero;
                _mode = mode;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
