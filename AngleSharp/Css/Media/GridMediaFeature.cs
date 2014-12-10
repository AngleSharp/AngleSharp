namespace AngleSharp.Css.Media
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using System;

    sealed class GridMediaFeature : MediaFeature
    {
        #region Fields

        Int32 _grid;

        #endregion

        #region ctor

        public GridMediaFeature()
            : base(FeatureNames.Grid)
        {
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            _grid = 0;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converters.PositiveIntegerConverter.TryConvert(value, m => _grid = m);
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }

        #endregion
    }
}
