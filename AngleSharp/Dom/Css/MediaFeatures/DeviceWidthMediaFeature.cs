namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using System;

    sealed class DeviceWidthMediaFeature : MediaFeature
    {
        #region ctor

        public DeviceWidthMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            // Default: Allowed
            get { return Converters.LengthConverter; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var length = Length.Zero;
            var desired = length.ToPixel();
            var available = (Single)device.DeviceWidth;

            if (IsMaximum)
            {
                return available <= desired;
            }
            else if (IsMinimum)
            {
                return available >= desired;
            }

            return desired == available;
        }

        #endregion
    }
}
