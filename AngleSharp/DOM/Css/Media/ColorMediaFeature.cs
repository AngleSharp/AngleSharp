namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class MinColorMediaFeature : MediaFeature
    {
        Int32 _color;

        public MinColorMediaFeature()
            : base(FeatureNames.MinColor)
        {
        }

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(ICssValue value)
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

        public override Boolean Validate(IWindow window)
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

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(ICssValue value)
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

        public override Boolean Validate(IWindow window)
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

        internal override Boolean TrySetDefaultValue()
        {
            _color = 1;
            return true;
        }

        internal override Boolean TrySetValue(ICssValue value)
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

        public override Boolean Validate(IWindow window)
        {
            return true;
        }
    }
}
