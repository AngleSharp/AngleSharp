namespace AngleSharp.Css.MediaFeatures
{
    using System;
    using AngleSharp.Dom.Css;

    sealed class OrientationMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<Boolean> TheConverter = Converters.Toggle(Keywords.Portrait, Keywords.Landscape);

        #endregion

        #region ctor

        public OrientationMediaFeature()
            : base(FeatureNames.Orientation)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            // Default: Allowed (value: false => landscape)
            get { return TheConverter; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var portrait = false;
            var desired = portrait;
            var available = device.DeviceHeight >= device.DeviceWidth;
            return desired == available;
        }

        #endregion
    }
}
