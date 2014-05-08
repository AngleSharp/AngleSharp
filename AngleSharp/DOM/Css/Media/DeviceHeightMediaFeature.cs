namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class MinDeviceHeightMediaFeature : MediaFeature
    {
        Length _length;

        public MinDeviceHeightMediaFeature()
            : base(FeatureNames.MinDeviceHeight)
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

    sealed class MaxDeviceHeightMediaFeature : MediaFeature
    {
        Length _length;

        public MaxDeviceHeightMediaFeature()
            : base(FeatureNames.MaxDeviceHeight)
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

    sealed class DeviceHeightMediaFeature : MediaFeature
    {
        Length _length;

        public DeviceHeightMediaFeature()
            : base(FeatureNames.DeviceHeight)
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
