namespace AngleSharp.Css.MediaFeatures
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class ScanMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<Boolean> Converter = Converters.Toggle(Keywords.Interlace, Keywords.Progressive);
        Boolean _interlace;

        #endregion

        #region ctor

        public ScanMediaFeature()
            : base(FeatureNames.Scan)
        {
            _interlace = false;
        }

        #endregion

        #region Properties

        public Boolean IsProgressive
        {
            get { return !_interlace; }
        }

        public Boolean IsInterlaced
        {
            get { return _interlace; }
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            _interlace = false;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converter.TryConvert(value, m => _interlace = m);
        }

        public override Boolean Validate(RenderDevice device)
        {
            var desired = _interlace;
            var available = device.IsInterlaced;
            return desired == available;
        }

        #endregion
    }
}
