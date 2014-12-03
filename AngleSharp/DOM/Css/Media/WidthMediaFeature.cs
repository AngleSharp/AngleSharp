namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class WidthMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<Length> Converter = CSSProperty.WithLength();
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
            return Converter.TryConvert(value, m => _length = m);
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }

        #endregion
    }
}
