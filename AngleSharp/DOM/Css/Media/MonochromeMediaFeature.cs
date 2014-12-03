namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Extensions;
    using System;

    sealed class MonochromeMediaFeature : MediaFeature
    {
        Int32 _index;

        public MonochromeMediaFeature(String name)
            : base(name)
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
