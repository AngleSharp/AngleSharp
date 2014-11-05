namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class OrientationMediaFeature : MediaFeature
    {
        Boolean _portrait;
        Boolean _landscape;

        public OrientationMediaFeature()
            : base(FeatureNames.Orientation)
        {
            _portrait = false;
            _landscape = false;
        }

        internal override Boolean TrySetDefaultValue()
        {
            _portrait = true;
            _landscape = true;
            return true;
        }

        internal override Boolean TrySetValue(CSSValue value)
        {
            if (Value.Is(Keywords.Portrait))
            {
                Value = value;
                _portrait = true;
                _landscape = false;
                return true;
            }
            else if (Value.Is(Keywords.Landscape))
            {
                Value = value;
                _portrait = false;
                _landscape = true;
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
