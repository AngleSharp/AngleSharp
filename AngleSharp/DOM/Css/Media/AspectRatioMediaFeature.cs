namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using System;

    sealed class MinAspectRatioMediaFeature : MediaFeature
    {
        Tuple<Int32, Int32> _ratio;

        public MinAspectRatioMediaFeature()
            : base(FeatureNames.MinAspectRatio)
        {
        }

        protected override Boolean TrySetDefault()
        {
            return false;
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

    sealed class MaxAspectRatioMediaFeature : MediaFeature
    {
        Tuple<Int32, Int32> _ratio;

        public MaxAspectRatioMediaFeature()
            : base(FeatureNames.MaxAspectRatio)
        {
        }

        protected override Boolean TrySetDefault()
        {
            return false;
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

    sealed class AspectRatioMediaFeature : MediaFeature
    {
        Tuple<Int32, Int32> _ratio;

        public AspectRatioMediaFeature()
            : base(FeatureNames.AspectRatio)
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
