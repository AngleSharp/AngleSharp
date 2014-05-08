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

        public override Boolean SetDefaultValue()
        {
            _progressive = true;
            _interlace = true;
            return true;
        }

        public override Boolean SetValue(CSSValue value)
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

        public override Boolean Validate()
        {
            return true;
        }
    }
}
