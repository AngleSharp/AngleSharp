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

        protected override Boolean TrySetDefault()
        {
            _progressive = true;
            _interlace = true;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            if (Value.Is(Keywords.Progressive))
            {
                _progressive = true;
                _interlace = false;
                return true;
            }
            else if (Value.Is(Keywords.Interlace))
            {
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
