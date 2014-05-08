namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class MinColorIndexMediaFeature : MediaFeature
    {
        Int32 _index;

        public MinColorIndexMediaFeature()
            : base(FeatureNames.MinColorIndex)
        {
        }

        public override Boolean SetDefaultValue()
        {
            return false;
        }

        public override Boolean SetValue(CSSValue value)
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

        public override Boolean Validate()
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

        public override Boolean SetDefaultValue()
        {
            return false;
        }

        public override Boolean SetValue(CSSValue value)
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

        public override Boolean Validate()
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

        public override Boolean SetDefaultValue()
        {
            _index = 0;
            return true;
        }

        public override Boolean SetValue(CSSValue value)
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

        public override Boolean Validate()
        {
            return true;
        }
    }
}
