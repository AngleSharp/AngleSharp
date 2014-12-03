namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using System;

    sealed class AspectRatioMediaFeature : MediaFeature
    {
        Tuple<Int32, Int32> _ratio;

        public AspectRatioMediaFeature(String name)
            : base(name)
        {
        }

        protected override Boolean TrySetDefault()
        {
            _ratio = Tuple.Create(1, 1);
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return CSSProperty.WithRatio().TryConvert(value, m => _ratio = m);
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }
    }
}
