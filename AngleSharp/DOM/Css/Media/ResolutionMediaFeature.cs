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

        protected override Boolean TrySetDefault()
        {
            return false;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var res = value.ToResolution();

            if (res.HasValue)
            {
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

        protected override Boolean TrySetDefault()
        {
            return false;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var res = value.ToResolution();

            if (res.HasValue)
            {
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

        protected override Boolean TrySetDefault()
        {
            _res = new Resolution(72f, Resolution.Unit.Dpi);
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var res = value.ToResolution();

            if (res.HasValue)
            {
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
