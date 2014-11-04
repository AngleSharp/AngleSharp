namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Extensions;
    using System;

    sealed class MinHeightMediaFeature : MediaFeature
    {
        Length _length;

        public MinHeightMediaFeature()
            : base(FeatureNames.MinHeight)
        {
        }

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(CSSValue value)
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

        public override Boolean Validate(IWindow window)
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

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(CSSValue value)
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

        public override Boolean Validate(IWindow window)
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

        internal override Boolean TrySetDefaultValue()
        {
            return true;
        }

        internal override Boolean TrySetValue(CSSValue value)
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

        public override Boolean Validate(IWindow window)
        {
            return true;
        }
    }
}
