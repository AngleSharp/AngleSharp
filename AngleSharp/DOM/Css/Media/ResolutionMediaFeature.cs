namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class MinResolutionMediaFeature : MediaFeature
    {
        Resolution _res;

        public MinResolutionMediaFeature()
            : base(FeatureNames.MinResolution)
        {
        }

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(ICssValue value)
        {
            var res = value.ToResolution();

            if (res.HasValue)
            {
                Value = value;
                _res = res.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }
    }

    sealed class MaxResolutionMediaFeature : MediaFeature
    {
        Resolution _res;

        public MaxResolutionMediaFeature()
            : base(FeatureNames.MaxResolution)
        {
        }

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(ICssValue value)
        {
            var res = value.ToResolution();

            if (res.HasValue)
            {
                Value = value;
                _res = res.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }
    }

    sealed class ResolutionMediaFeature : MediaFeature
    {
        Resolution _res;

        public ResolutionMediaFeature()
            : base(FeatureNames.Resolution)
        {
        }

        internal override Boolean TrySetDefaultValue()
        {
            _res = new Resolution(72f, Resolution.Unit.Dpi);
            return true;
        }

        internal override Boolean TrySetValue(ICssValue value)
        {
            var res = value.ToResolution();

            if (res.HasValue)
            {
                Value = value;
                _res = res.Value;
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
