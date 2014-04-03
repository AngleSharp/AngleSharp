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

        static readonly ValueConverter<LineHeightMode> _modes = new ValueConverter<LineHeightMode>();
        static readonly NormalLineHeightMode _normal = new NormalLineHeightMode();
        static readonly CSSLineHeightProperty _default = new CSSLineHeightProperty();
        LineHeightMode _mode;

        #endregion

        #region ctor

        static CSSLineHeightProperty()
        {
            _modes.AddStatic("normal", _normal);
            _modes.AddConstructed<RelativeLineHeightMode>();
            _modes.AddConstructed<AbsoluteLineHeightMode>();
            _modes.AddConstructed<MultipleLineHeightMode>();
        }

        public CSSLineHeightProperty()
            : base(PropertyNames.LINE_HEIGHT)
        {
            _inherited = true;
            _mode = _normal;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default line height.
        /// </summary>
        public static CSSLineHeightProperty Default
        {
            get { return _default; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            LineHeightMode mode;

            if (_modes.TryCreate(value, out mode))
                _mode = mode;
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
        /// Relative to the font size of the element itself. The computed
        /// value is this percentage multiplied by the element's computed font size.
        /// </summary>
        sealed class RelativeLineHeightMode : LineHeightMode
        {
            Single _scale;

            public RelativeLineHeightMode(CSSPercentValue percent)
            {
                _scale = percent.Value;
            }
        }

        /// <summary>
        /// The specified length is used in the calculation of the line box
        /// height. See length values for possible units.
        /// </summary>
        sealed class AbsoluteLineHeightMode : LineHeightMode
        {
            Length _length;

            public AbsoluteLineHeightMode(Length length)
            {
                _length = length;
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
