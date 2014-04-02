namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight
    /// </summary>
    public sealed class CSSFontWeightProperty : CSSProperty
    {
        #region Fields

        static readonly ValueConverter<FontWeightMode> _weights = new ValueConverter<FontWeightMode>();
        static readonly NormalWeightMode _normal = new NormalWeightMode();
        FontWeightMode _weight;

        #endregion

        #region ctor

        static CSSFontWeightProperty()
        {
            _weights.AddStatic("normal", _normal);
            _weights.AddStatic("bold", new BoldWeightMode());
            _weights.AddStatic("bolder", new BolderWeightMode());
            _weights.AddStatic("lighter", new LighterWeightMode());
            _weights.AddConstructed<NumberWeightMode>();
        }

        public CSSFontWeightProperty()
            : base(PropertyNames.FONT_WEIGHT)
        {
            _weight = _normal;
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            FontWeightMode weight;

            if (_weights.TryCreate(value, out weight))
                _weight = weight;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Mode

        abstract class FontWeightMode
        { }

        sealed class NormalWeightMode : FontWeightMode
        {
            // 400
        }

        sealed class BoldWeightMode : FontWeightMode
        {
            // 700
        }

        sealed class LighterWeightMode : FontWeightMode
        {
        }

        sealed class BolderWeightMode : FontWeightMode
        {
        }

        sealed class NumberWeightMode : FontWeightMode
        {
            Int32 _weight;

            public NumberWeightMode(Single weight)
            {
                _weight = (Int32)Math.Min(900, Math.Max(100, Math.Round(weight * 0.01) * 100f));
            }
        }

        #endregion
    }
}
