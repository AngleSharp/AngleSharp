namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class GridMediaFeature : MediaFeature
    {
        Int32 _grid;

        public GridMediaFeature()
            : base(FeatureNames.Grid)
        {
        }

        protected override Boolean TrySetDefault()
        {
            _grid = 0;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var grid = value.ToInteger();

            if (grid.HasValue && grid.Value >= 0)
            {
                _grid = grid.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }
    }
}
