namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Basis for all elementary margin properties.
    /// </summary>
    class CSSMarginPartProperty : CSSProperty
    {
        #region Fields

        static readonly AutoMarginMode _auto = new AutoMarginMode();
        static readonly AbsoluteMarginMode _default = new AbsoluteMarginMode(Length.Zero);
        MarginMode _mode;

        #endregion

        #region ctor

        protected CSSMarginPartProperty(String name)
            : base(name)
        {
            _inherited = false;
            _mode = _default;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
                _mode = new AbsoluteMarginMode(length.Value);
            else if (value is CSSPercentValue)
                _mode = new RelativeMarginMode(((CSSPercentValue)value).Value);
            else if (value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals("auto", StringComparison.OrdinalIgnoreCase))
                _mode = _auto;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Mode

        abstract class MarginMode
        {
            //TODO add members
        }

        /// <summary>
        /// auto is replaced by some suitable value, e.g. it can be used for centering of blocks.
        /// </summary>
        sealed class AutoMarginMode : MarginMode
        {
        }

        /// <summary>
        /// Relative to the width of the containing block. Negative values are allowed.
        /// </summary>
        sealed class RelativeMarginMode : MarginMode
        {
            Single _percent;

            public RelativeMarginMode(Single percent)
            {
                _percent = percent;
            }
        }

        /// <summary>
        /// Specifies a fixed width. Negative Values are allowed.
        /// </summary>
        sealed class AbsoluteMarginMode : MarginMode
        {
            Length _length;

            public AbsoluteMarginMode(Length length)
            {
                _length = length;
            }
        }

        #endregion
    }
}
