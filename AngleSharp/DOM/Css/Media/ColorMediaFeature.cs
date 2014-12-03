namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Extensions;
    using System;

    sealed class ColorMediaFeature : MediaFeature
    {
        Int32 _color;

        public ColorMediaFeature(String name)
            : base(name)
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
