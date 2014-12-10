namespace AngleSharp.Css.Media
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using System;

    sealed class MonochromeMediaFeature : MediaFeature
    {
        #region Fields

        Int32 _index;

        #endregion

        #region ctor

        public MonochromeMediaFeature(String name)
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

        public override Boolean Validate(IWindow window)
        {
            return true;
        }

        #endregion
    }
}
