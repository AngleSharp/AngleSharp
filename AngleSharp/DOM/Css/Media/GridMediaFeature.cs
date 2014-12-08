namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class GridMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<Int32> Converter = Converters.IntegerConverter.Constraint(m => m >= 0);
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
            return Converter.TryConvert(value, m => _grid = m);
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }

        #endregion
    }
}
