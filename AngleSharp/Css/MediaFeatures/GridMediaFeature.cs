namespace AngleSharp.Css.MediaFeatures
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class GridMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<Int32> Converter = Converters.IntegerConverter.Constraint(m => m == 1 || m == 0);
        Boolean _grid;

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
            _grid = false;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converter.TryConvert(value, m => _grid = m == 1);
        }

        public override Boolean Validate(RenderDevice device)
        {
            var desired = _grid;
            var available = device.IsGrid;
            return desired == available;
        }

        #endregion
    }
}
