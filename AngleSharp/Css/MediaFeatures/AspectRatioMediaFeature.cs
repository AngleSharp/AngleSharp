namespace AngleSharp.Css.MediaFeatures
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class AspectRatioMediaFeature : MediaFeature
    {
        #region Fields

        Tuple<Int32, Int32> _ratio;

        #endregion

        #region ctor

        public AspectRatioMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            return false;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converters.RatioConverter.TryConvert(value, m => _ratio = m);
        }

        public override Boolean Validate(RenderDevice device)
        {
            var desired = (Single)_ratio.Item1 / (Single)_ratio.Item2;
            var available = (Single)device.ViewPortWidth / (Single)device.ViewPortHeight;

            if (IsMaximum)
                return available <= desired;
            else if (IsMinimum)
                return available >= desired;
            
            return desired == available;
        }

        #endregion
    }
}
