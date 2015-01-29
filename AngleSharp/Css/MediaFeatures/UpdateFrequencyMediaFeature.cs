namespace AngleSharp.Css.MediaFeatures
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class UpdateFrequencyMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<UpdateFrequency> Converter = Map.UpdateFrequencies.ToConverter();
        UpdateFrequency _frequency;

        #endregion

        #region ctor

        public UpdateFrequencyMediaFeature()
            : base(FeatureNames.UpdateFrequency)
        {
            _frequency = UpdateFrequency.Normal;
        }

        #endregion

        #region Properties

        public UpdateFrequency Frequency
        {
            get { return _frequency; }
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            _frequency = UpdateFrequency.Normal;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converter.TryConvert(value, m => _frequency = m);
        }

        public override Boolean Validate(RenderDevice device)
        {
            var desired = _frequency;
            var available = device.Frequency;

            if (available >= 30)
                return desired == UpdateFrequency.Normal;
            else if (available > 0)
                return desired == UpdateFrequency.Slow;

            return desired == UpdateFrequency.None;
        }

        #endregion
    }
}
