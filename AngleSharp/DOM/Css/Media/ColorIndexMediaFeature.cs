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

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(ICssValue value)
        {
            var index = value.ToInteger();

            if (index.HasValue && index.Value >= 0)
            {
                Value = value;
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

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(ICssValue value)
        {
            var index = value.ToInteger();

            if (index.HasValue && index.Value >= 0)
            {
                Value = value;
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

        internal override Boolean TrySetDefaultValue()
        {
            _index = 0;
            return true;
        }

        internal override Boolean TrySetValue(ICssValue value)
        {
            var index = value.ToInteger();

            if (index.HasValue && index.Value >= 0)
            {
                Value = value;
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
