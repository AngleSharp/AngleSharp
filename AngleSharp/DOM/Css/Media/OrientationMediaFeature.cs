namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class OrientationMediaFeature : MediaFeature
    {
        Boolean _portrait;
        Boolean _landscape;

        public OrientationMediaFeature()
            : base(FeatureNames.Orientation)
        {
        }

        internal override Boolean TrySetDefaultValue()
        {
            _portrait = true;
            _landscape = true;
            return true;
        }

        internal override Boolean TrySetValue(CSSValue value)
        {
            if (Value.Is("portrait"))
            {
                Value = value;
                _portrait = true;
                _landscape = false;
                return true;
            }
            else if (Value.Is("landscape"))
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
