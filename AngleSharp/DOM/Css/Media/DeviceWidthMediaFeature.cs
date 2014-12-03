namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class MinDeviceWidthMediaFeature : MediaFeature
    {
        Length _length;

        public MinDeviceWidthMediaFeature()
            : base(FeatureNames.MinDeviceWidth)
        {
        }

        protected override Boolean TrySetDefault()
        {
            return false;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
            {
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

        protected override Boolean TrySetDefault()
        {
            return false;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
            {
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

        protected override Boolean TrySetDefault()
        {
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
            {
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
