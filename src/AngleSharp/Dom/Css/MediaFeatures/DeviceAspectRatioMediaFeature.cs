namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    sealed class DeviceAspectRatioMediaFeature : MediaFeature
    {
        #region ctor

        public DeviceAspectRatioMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            // Default: NOT Allowed
            get { return Converters.RatioConverter; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var ratio = Tuple.Create(1f, 1f);
            var desired = ratio.Item1 / ratio.Item2;
            var available = device.DeviceWidth / (Single)device.DeviceHeight;

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
