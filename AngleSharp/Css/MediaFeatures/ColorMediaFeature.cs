namespace AngleSharp.Css.MediaFeatures
{
    using System;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;

    sealed class ColorMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<Int32> TheConverter = Converters.IntegerConverter.Constraint(m => m > 0);

        #endregion

        #region ctor

        public ColorMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            // Default: 1
            get { return TheConverter; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var color = 1;
            var desired = color;
            var available = Math.Pow(device.ColorBits, 2);

            if (IsMaximum)
                return available <= desired;
            else if (IsMinimum)
                return available >= desired;

            return desired == available;
        }

        #endregion
    }
}
