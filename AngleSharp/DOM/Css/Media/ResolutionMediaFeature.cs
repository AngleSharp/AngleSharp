namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class ResolutionMediaFeature : MediaFeature
    {
        #region Fields

        Resolution _res;

        #endregion

        #region ctor

        public ResolutionMediaFeature(String name)
            : base(name)
        {
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            _res = new Resolution(72f, Resolution.Unit.Dpi);
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converters.ResolutionConverter.TryConvert(value, m => _res = m);
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }

        #endregion
    }
}
