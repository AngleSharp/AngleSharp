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

        internal override Boolean TrySetDefaultValue()
        {
            return false;
        }

        internal override Boolean TrySetValue(CSSValue value)
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

        public override Boolean Validate(IWindow window)
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

    sealed class WidthMediaFeature : MediaFeature
    {
        Length _length;

        public WidthMediaFeature()
            : base(FeatureNames.Width)
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
