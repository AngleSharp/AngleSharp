namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class ColorMediaFeature : MediaFeature
    {
        #region ctor

        public ColorMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            get 
            {
                return IsMinimum || IsMaximum ? 
                    Converters.PositiveIntegerConverter : 
                    Converters.PositiveIntegerConverter.Option(1);
            }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var color = 1;
            var desired = color;
            var available = Math.Pow(device.ColorBits, 2);

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
