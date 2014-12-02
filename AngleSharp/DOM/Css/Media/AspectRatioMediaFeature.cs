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

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(ICssValue value)
        {
            return CSSProperty.WithRatio().TryConvert(value, m =>
            {
                _ratio = m;
                Value = value;
            });
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

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(ICssValue value)
        {
            return CSSProperty.WithRatio().TryConvert(value, m =>
            {
                _ratio = m;
                Value = value;
            });
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

        internal override Boolean TrySetDefaultValue()
        {
            _ratio = Tuple.Create(1, 1);
            return true;
        }

        internal override Boolean TrySetValue(ICssValue value)
        {
            return CSSProperty.WithRatio().TryConvert(value, m =>
            {
                _ratio = m;
                Value = value;
            });
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }
    }
}
