namespace AngleSharp.Css.MediaFeatures
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class OrientationMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<Boolean> Converter = Converters.Toggle(Keywords.Portrait, Keywords.Landscape);
        Boolean _portrait;

        #endregion

        #region ctor

        public OrientationMediaFeature()
            : base(FeatureNames.Orientation)
        {
            _portrait = false;
        }

        #endregion

        #region Properties

        public Boolean IsLandscape
        {
            get { return !_portrait; }
        }

        public Boolean IsPortrait
        {
            get { return _portrait; }
        }

        #endregion

        #region Methods

        internal override Boolean TrySetDefault()
        {
            _portrait = false;
            return true;
        }

        internal override Boolean TrySetCustom(CssValue value)
        {
            return Converter.TryConvert(value, m => _portrait = m);
        }

        public override Boolean Validate(RenderDevice device)
        {
            var desired = _portrait;
            var available = device.DeviceHeight >= device.DeviceWidth;
            return desired == available;
        }

        #endregion
    }
}
