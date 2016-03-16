namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class UpdateFrequencyMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter TheConverter = Map.UpdateFrequencies.ToConverter();

        #endregion

        #region ctor

        public UpdateFrequencyMediaFeature()
            : base(FeatureNames.UpdateFrequency)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            // Default: UpdateFrequency.Normal
            get { return TheConverter; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var frequency = UpdateFrequency.Normal;
            var desired = frequency;
            var available = device.Frequency;

            if (available >= 30)
            {
                return desired == UpdateFrequency.Normal;
            }
            else if (available > 0)
            {
                return desired == UpdateFrequency.Slow;
            }

            return desired == UpdateFrequency.None;
        }

        #endregion
    }
}
