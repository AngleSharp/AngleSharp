namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class MinDeviceHeightMediaFeature : MediaFeature
    {
        Length _length;

        public MinDeviceHeightMediaFeature()
            : base(FeatureNames.MinDeviceHeight)
        {
        }

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(ICssValue value)
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

        public override Boolean Validate(IWindow window)
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

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(ICssValue value)
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

        public override Boolean Validate(IWindow window)
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

        internal override Boolean TrySetDefaultValue()
        {
            return true;
        }

        internal override Boolean TrySetValue(ICssValue value)
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

        public override Boolean Validate(IWindow window)
        {
            return true;
        }
    }
}
