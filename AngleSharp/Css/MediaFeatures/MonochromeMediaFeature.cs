namespace AngleSharp.Css.MediaFeatures
{
    using System;
    using AngleSharp.Dom.Css;

    sealed class MonochromeMediaFeature : MediaFeature
    {
        #region ctor

        public MonochromeMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            // Default: 0
            get { return Converters.PositiveIntegerConverter; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var index = 0;
            var desired = index;
            var available = device.MonochromeBits;

            if (IsMaximum)
                return available <= desired;
            else if (IsMinimum)
                return available >= desired;

            return desired == available;
        }

        #endregion
    }
}
