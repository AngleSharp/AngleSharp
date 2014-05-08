namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class MinColorMediaFeature : MediaFeature
    {
        Int32 _color;

        public MinColorMediaFeature()
            : base(FeatureNames.MinColor)
        {
        }

        public override Boolean SetDefaultValue()
        {
            return false;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var color = value.ToInteger();

            if (color.HasValue && color.Value > 0)
            {
                Value = value;
                _color = color.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate()
        {
            return true;
        }
    }

    sealed class MaxColorMediaFeature : MediaFeature
    {
        Int32 _color;

        public MaxColorMediaFeature()
            : base(FeatureNames.MaxColor)
        {
        }

        public override Boolean SetDefaultValue()
        {
            return false;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var color = value.ToInteger();

            if (color.HasValue && color.Value > 0)
            {
                Value = value;
                _color = color.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate()
        {
            return true;
        }
    }

    sealed class ColorMediaFeature : MediaFeature
    {
        Int32 _color;

        public ColorMediaFeature()
            : base(FeatureNames.Color)
        {
        }

        public override Boolean SetDefaultValue()
        {
            _color = 1;
            return true;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var color = value.ToInteger();

            if (color.HasValue && color.Value > 0)
            {
                Value = value;
                _color = color.Value;
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
