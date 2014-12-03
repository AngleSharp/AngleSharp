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

    sealed class MaxDeviceHeightMediaFeature : MediaFeature
    {
        Length _length;

        public MaxDeviceHeightMediaFeature()
            : base(FeatureNames.MaxDeviceHeight)
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

    sealed class DeviceHeightMediaFeature : MediaFeature
    {
        Length _length;

        public DeviceHeightMediaFeature()
            : base(FeatureNames.DeviceHeight)
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
