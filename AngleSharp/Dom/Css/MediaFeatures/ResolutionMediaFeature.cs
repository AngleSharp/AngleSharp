namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using System;

    sealed class ResolutionMediaFeature : MediaFeature
    {
        #region ctor

        public ResolutionMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            // Default: new Resolution(72f, Resolution.Unit.Dpi)
            get { return Converters.ResolutionConverter; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var res = new Resolution(72f, Resolution.Unit.Dpi);
            var desired = res.To(Resolution.Unit.Dpi);
            var available = (Single)device.Resolution;

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
