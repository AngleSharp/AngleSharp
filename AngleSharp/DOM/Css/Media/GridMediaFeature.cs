namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class GridMediaFeature : MediaFeature
    {
        Int32 _grid;

        public GridMediaFeature()
            : base(FeatureNames.Grid)
        {
        }

        public override Boolean SetDefaultValue()
        {
            _grid = 0;
            return true;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var grid = value.ToInteger();

            if (grid.HasValue && grid.Value >= 0)
            {
                Value = value;
                _grid = grid.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate()
        {
            return true;
        }
    }
}
