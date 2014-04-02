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

        sealed class NormalLineHeightMode : LineHeightMode
        { }

        sealed class RelativeLineHeightMode : LineHeightMode
        {
            Single _scale;

            public RelativeLineHeightMode(CSSPercentValue percent)
            {
                _scale = percent.Value;
            }
        }

        sealed class AbsoluteLineHeightMode : LineHeightMode
        {
            Length _length;

            public AbsoluteLineHeightMode(Length length)
            {
                _length = length;
            }
        }

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
