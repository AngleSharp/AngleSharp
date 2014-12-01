namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class MinAspectRatioMediaFeature : MediaFeature
    {
        Single _ratio;

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
            var ratio = value.ToAspectRatio();

            if (ratio.HasValue)
            {
                Value = value;
                _ratio = ratio.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate(IWindow window)
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

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(ICssValue value)
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

        public override Boolean Validate(IWindow window)
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

        internal override Boolean TrySetDefaultValue()
        {
            _ratio = 1f;
            return true;
        }

        internal override Boolean TrySetValue(ICssValue value)
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

        public override Boolean Validate(IWindow window)
        {
            return true;
        }
    }
}
