namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class AspectRatioMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<Tuple<Int32, Int32>> Converter = CSSProperty.WithRatio();
        Tuple<Int32, Int32> _ratio;

        #endregion

        #region ctor

        public AspectRatioMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            _ratio = Tuple.Create(1, 1);
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converter.TryConvert(value, m => _ratio = m);
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }

        #endregion
    }
}
