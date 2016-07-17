namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using static AngleSharp.Css.Converters;

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
            get { return IsMinimum || IsMaximum ? NaturalIntegerConverter : NaturalIntegerConverter.Option(1); }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var index = 0;
            var desired = index;
            var available = device.MonochromeBits;

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
