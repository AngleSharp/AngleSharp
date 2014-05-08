namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class MinMonochromeMediaFeature : MediaFeature
    {
        Int32 _index;

        public MinMonochromeMediaFeature()
            : base(FeatureNames.MinMonochrome)
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

    sealed class MaxMonochromeMediaFeature : MediaFeature
    {
        Int32 _index;

        public MaxMonochromeMediaFeature()
            : base(FeatureNames.MaxMonochrome)
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

    sealed class MonochromeMediaFeature : MediaFeature
    {
        Int32 _index;

        public MonochromeMediaFeature()
            : base(FeatureNames.Monochrome)
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
