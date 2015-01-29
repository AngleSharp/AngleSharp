namespace AngleSharp.Css.MediaFeatures
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class ResolutionMediaFeature : MediaFeature
    {
        #region Fields

        Resolution _res;

        #endregion

        #region ctor

        public ResolutionMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            _res = new Resolution(72f, Resolution.Unit.Dpi);
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converters.ResolutionConverter.TryConvert(value, m => _res = m);
        }

        public override Boolean Validate(RenderDevice device)
        {
            var desired = _res.To(Resolution.Unit.Dpi);
            var available = (Single)device.Resolution;

            if (IsMaximum)
                return available <= desired;
            else if (IsMinimum)
                return available >= desired;

            return desired == available;
        }

        #endregion
    }
}
