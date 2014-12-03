namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class ResolutionMediaFeature : MediaFeature
    {
        Resolution _res;

        public ResolutionMediaFeature(String name)
            : base(name)
        {
        }

        protected override Boolean TrySetDefault()
        {
            _res = new Resolution(72f, Resolution.Unit.Dpi);
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            var res = value.ToResolution();

            if (res.HasValue)
            {
                _res = res.Value;
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
