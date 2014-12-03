namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class DeviceHeightMediaFeature : MediaFeature
    {
        Length _length;

        public DeviceHeightMediaFeature(String name)
            : base(name)
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
