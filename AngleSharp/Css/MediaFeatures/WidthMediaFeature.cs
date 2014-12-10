namespace AngleSharp.Css.MediaFeatures
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using System;

    sealed class WidthMediaFeature : MediaFeature
    {
        #region Fields

        Length _length;

        #endregion

        #region ctor

        public WidthMediaFeature(String name)
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

        public override Boolean Validate(IWindow window)
        {
            return true;
        }

        #endregion
    }
}
