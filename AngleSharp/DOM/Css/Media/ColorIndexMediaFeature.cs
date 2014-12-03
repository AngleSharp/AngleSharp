namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class MinColorIndexMediaFeature : MediaFeature
    {
        Int32 _index;

        public MinColorIndexMediaFeature()
            : base(FeatureNames.MinColorIndex)
        {
        }

        protected override Boolean TrySetDefault()
        {
            return false;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var index = value.ToInteger();

            if (index.HasValue && index.Value >= 0)
            {
                _index = index.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }
    }

    sealed class MaxColorIndexMediaFeature : MediaFeature
    {
        Int32 _index;

        public MaxColorIndexMediaFeature()
            : base(FeatureNames.MaxColorIndex)
        {
        }

        protected override Boolean TrySetDefault()
        {
            return false;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var index = value.ToInteger();

            if (index.HasValue && index.Value >= 0)
            {
                _index = index.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }
    }

    sealed class ColorIndexMediaFeature : MediaFeature
    {
        Int32 _index;

        public ColorIndexMediaFeature()
            : base(FeatureNames.ColorIndex)
        {
        }

        protected override Boolean TrySetDefault()
        {
            _index = 0;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var index = value.ToInteger();

            if (index.HasValue && index.Value >= 0)
            {
                _index = index.Value;
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
