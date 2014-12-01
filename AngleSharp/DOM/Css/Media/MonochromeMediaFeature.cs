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

    sealed class MaxMonochromeMediaFeature : MediaFeature
    {
        Int32 _index;

        public MaxMonochromeMediaFeature()
            : base(FeatureNames.MaxMonochrome)
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

    sealed class MonochromeMediaFeature : MediaFeature
    {
        Int32 _index;

        public MonochromeMediaFeature()
            : base(FeatureNames.Monochrome)
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
