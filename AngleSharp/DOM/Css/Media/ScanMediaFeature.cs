namespace AngleSharp.DOM.Css.Media
{
    using System;

    sealed class ScanMediaFeature : MediaFeature
    {
        Boolean _progressive;
        Boolean _interlace;

        public ScanMediaFeature()
            : base(FeatureNames.Scan)
        {
        }

        internal override Boolean TrySetDefaultValue()
        {
            _progressive = true;
            _interlace = true;
            return true;
        }

        internal override Boolean TrySetValue(CSSValue value)
        {
            if (Value.Is("progressive"))
            {
                Value = value;
                _progressive = true;
                _interlace = false;
                return true;
            }
            else if (Value.Is("interlace"))
            {
                Value = value;
                _progressive = false;
                _interlace = true;
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
