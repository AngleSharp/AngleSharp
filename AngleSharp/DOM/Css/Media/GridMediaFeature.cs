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

        internal override Boolean TrySetDefaultValue()
        {
            _grid = 0;
            return true;
        }

        internal override Boolean TrySetValue(CSSValue value)
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

        public override Boolean Validate(IWindow window)
        {
            return true;
        }
    }
}
