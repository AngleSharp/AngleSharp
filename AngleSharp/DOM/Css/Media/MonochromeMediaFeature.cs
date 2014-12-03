namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class MinMonochromeMediaFeature : MediaFeature
    {
        Int32 _index;

        public MinMonochromeMediaFeature()
            : base(FeatureNames.MinMonochrome)
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

    sealed class MaxMonochromeMediaFeature : MediaFeature
    {
        Int32 _index;

        public MaxMonochromeMediaFeature()
            : base(FeatureNames.MaxMonochrome)
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

    sealed class MonochromeMediaFeature : MediaFeature
    {
        Int32 _index;

        public MonochromeMediaFeature()
            : base(FeatureNames.Monochrome)
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
