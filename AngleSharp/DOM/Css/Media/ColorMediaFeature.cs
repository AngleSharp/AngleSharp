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

        protected override Boolean TrySetDefault()
        {
            return false;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var color = value.ToInteger();

            if (color.HasValue && color.Value > 0)
            {
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

        protected override Boolean TrySetDefault()
        {
            return false;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var color = value.ToInteger();

            if (color.HasValue && color.Value > 0)
            {
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

        protected override Boolean TrySetDefault()
        {
            _color = 1;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var color = value.ToInteger();

            if (color.HasValue && color.Value > 0)
            {
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
