namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class MinAspectRatioMediaFeature : MediaFeature
    {
        Single _ratio;

        public MinAspectRatioMediaFeature()
            : base(FeatureNames.MinAspectRatio)
        {
        }

        public override Boolean SetDefaultValue()
        {
            return false;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var ratio = value.ToAspectRatio();

            if (ratio.HasValue)
            {
                Value = value;
                _ratio = ratio.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate()
        {
            return true;
        }
    }

    sealed class MaxAspectRatioMediaFeature : MediaFeature
    {
        Single _ratio;

        public MaxAspectRatioMediaFeature()
            : base(FeatureNames.MaxAspectRatio)
        {
        }

        public override Boolean SetDefaultValue()
        {
            return false;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var ratio = value.ToAspectRatio();

            if (ratio.HasValue)
            {
                Value = value;
                _ratio = ratio.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate()
        {
            return true;
        }
    }

    sealed class AspectRatioMediaFeature : MediaFeature
    {
        Single _ratio;

        public AspectRatioMediaFeature()
            : base(FeatureNames.AspectRatio)
        {
        }

        public override Boolean SetDefaultValue()
        {
            _ratio = 1f;
            return true;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var ratio = value.ToAspectRatio();

            if (ratio.HasValue)
            {
                Value = value;
                _ratio = ratio.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate()
        {
            return true;
        }
    }
}
