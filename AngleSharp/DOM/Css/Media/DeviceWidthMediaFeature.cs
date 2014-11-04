namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Extensions;
    using System;

    sealed class MinDeviceWidthMediaFeature : MediaFeature
    {
        Length _length;

        public MinDeviceWidthMediaFeature()
            : base(FeatureNames.MinDeviceWidth)
        {
        }

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(CSSValue value)
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

    sealed class MaxDeviceWidthMediaFeature : MediaFeature
    {
        Length _length;

        public MaxDeviceWidthMediaFeature()
            : base(FeatureNames.MaxDeviceWidth)
        {
        }

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(CSSValue value)
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

    sealed class DeviceWidthMediaFeature : MediaFeature
    {
        Length _length;

        public DeviceWidthMediaFeature()
            : base(FeatureNames.DeviceWidth)
        {
        }

        internal override Boolean TrySetDefaultValue()
        {
            return true;
        }

        internal override Boolean TrySetValue(CSSValue value)
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
