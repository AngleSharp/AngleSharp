namespace AngleSharp.Css.MediaFeatures
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class ColorIndexMediaFeature : MediaFeature
    {
        #region Fields

        Int32 _index;

        #endregion

        #region ctor

        public ColorIndexMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            _index = 0;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converters.PositiveIntegerConverter.TryConvert(value, m => _index = m);
        }

        public override Boolean Validate(RenderDevice device)
        {
            var desired = _index;
            var available = device.ColorBits;

            if (IsMaximum)
                return available <= desired;
            else if (IsMinimum)
                return available >= desired;

            return desired == available;
        }

        #endregion
    }
}
