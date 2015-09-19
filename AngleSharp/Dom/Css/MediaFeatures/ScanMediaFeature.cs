namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    sealed class ScanMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter TheConverter = Converters.Toggle(Keywords.Interlace, Keywords.Progressive);

        #endregion

        #region ctor

        public ScanMediaFeature()
            : base(FeatureNames.Scan)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            // Default: Allowed (value: false => progressive)
            get { return TheConverter; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var interlace = false;
            var desired = interlace;
            var available = device.IsInterlaced;
            return desired == available;
        }

        #endregion
    }
}
