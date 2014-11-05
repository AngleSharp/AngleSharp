namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class ScanMediaFeature : MediaFeature
    {
        #region Fields

        Boolean _progressive;
        Boolean _interlace;

        #endregion

        #region ctor

        public ScanMediaFeature()
            : base(FeatureNames.Scan)
        {
            _progressive = false;
            _interlace = false;
        }

        #endregion

        #region Methods

        internal override Boolean TrySetDefaultValue()
        {
            _progressive = true;
            _interlace = true;
            return true;
        }

        internal override Boolean TrySetValue(CSSValue value)
        {
            if (Value.Is(Keywords.Progressive))
            {
                Value = value;
                _progressive = true;
                _interlace = false;
                return true;
            }
            else if (Value.Is(Keywords.Interlace))
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

        #endregion
    }
}
