namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using System;

    sealed class HeightMediaFeature : MediaFeature
    {
        #region ctor

        public HeightMediaFeature(String name)
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
            var available = (Single)device.ViewPortHeight;

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
