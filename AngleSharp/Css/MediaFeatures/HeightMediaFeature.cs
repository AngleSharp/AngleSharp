namespace AngleSharp.Css.MediaFeatures
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class HeightMediaFeature : MediaFeature
    {
        #region Fields

        Length _length;

        #endregion

        #region ctor

        public HeightMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converters.LengthConverter.TryConvert(value, m => _length = m);
        }

        public override Boolean Validate(RenderDevice device)
        {
            var desired = _length.ToPixel();
            var available = (Single)device.ViewPortHeight;

            if (IsMaximum)
                return available <= desired;
            else if (IsMinimum)
                return available >= desired;

            return desired == available;
        }

        #endregion
    }
}
