namespace AngleSharp.Css.Media
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using System;

    sealed class AspectRatioMediaFeature : MediaFeature
    {
        #region Fields

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
            return Converters.RatioConverter.TryConvert(value, m => _ratio = m);
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }

        #endregion
    }
}
