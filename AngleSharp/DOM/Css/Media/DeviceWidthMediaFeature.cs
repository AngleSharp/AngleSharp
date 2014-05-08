namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class MinDeviceWidthMediaFeature : MediaFeature
    {
        Length _length;

        public MinDeviceWidthMediaFeature()
            : base(FeatureNames.MinDeviceWidth)
        {
        }

        public override Boolean SetDefaultValue()
        {
            return false;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
            {
                Value = value;
                _length = length.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate()
        {
            return true;
        }
    }

    sealed class MaxDeviceWidthMediaFeature : MediaFeature
    {
        Length _length;

        public MaxDeviceWidthMediaFeature()
            : base(FeatureNames.MaxDeviceWidth)
        {
        }

        public override Boolean SetDefaultValue()
        {
            return false;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
            {
                Value = value;
                _length = length.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate()
        {
            return true;
        }
    }

    sealed class DeviceWidthMediaFeature : MediaFeature
    {
        Length _length;

        public DeviceWidthMediaFeature()
            : base(FeatureNames.DeviceWidth)
        {
        }

        public override Boolean SetDefaultValue()
        {
            return true;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
            {
                Value = value;
                _length = length.Value;
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
