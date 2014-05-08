namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class MinHeightMediaFeature : MediaFeature
    {
        Length _length;

        public MinHeightMediaFeature()
            : base(FeatureNames.MinHeight)
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

    sealed class MaxHeightMediaFeature : MediaFeature
    {
        Length _length;

        public MaxHeightMediaFeature()
            : base(FeatureNames.MaxHeight)
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

    sealed class HeightMediaFeature : MediaFeature
    {
        Length _length;

        public HeightMediaFeature()
            : base(FeatureNames.Height)
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
