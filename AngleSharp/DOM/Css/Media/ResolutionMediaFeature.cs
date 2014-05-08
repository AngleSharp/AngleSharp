namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class MinResolutionMediaFeature : MediaFeature
    {
        Resolution _res;

        public MinResolutionMediaFeature()
            : base(FeatureNames.MinResolution)
        {
        }

        public override Boolean SetDefaultValue()
        {
            return false;
        }

        public override Boolean SetValue(CSSValue value)
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

        public override Boolean Validate()
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

        public override Boolean SetDefaultValue()
        {
            return false;
        }

        public override Boolean SetValue(CSSValue value)
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

        public override Boolean Validate()
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

        public override Boolean SetDefaultValue()
        {
            _res = new Resolution(72f, DOM.Resolution.Unit.Dpi);
            return true;
        }

        public override Boolean SetValue(CSSValue value)
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

        public override Boolean Validate()
        {
            return true;
        }
    }
}
