namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class MinWidthMediaFeature : MediaFeature
    {
        Length _length;

        public MinWidthMediaFeature()
            : base(FeatureNames.MinWidth)
        {
        }

        public override Boolean SetDefaultValue()
        {
            return false;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
            {
                _length = length.Value;
                Value = value;
                return true;
            }

            return false;
        }

        public override Boolean Validate()
        {
            return true;
        }
    }

    sealed class MaxWidthMediaFeature : MediaFeature
    {
        Length _length;

        public MaxWidthMediaFeature()
            : base(FeatureNames.MaxWidth)
        {
        }

        public override Boolean SetDefaultValue()
        {
            return false;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
            {
                Value = value;
                _length = length.Value;
                return true;
            }

            return false;
        }

        public override Boolean Validate()
        {
            return true;
        }
    }

    sealed class WidthMediaFeature : MediaFeature
    {
        Length _length;

        public WidthMediaFeature()
            : base(FeatureNames.Width)
        {
        }

        public override Boolean SetDefaultValue()
        {
            return true;
        }

        public override Boolean SetValue(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
            {
                Value = value;
                _length = length.Value;
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
