namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    sealed class DevicePixelRatioFeature : MediaFeature
    {
        #region ctor

        public DevicePixelRatioFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            // Default: 1f
            get { return Converters.NaturalNumberConverter; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var value = 1f;
            var desired = value;
            var available = device.Resolution / 96f;

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
