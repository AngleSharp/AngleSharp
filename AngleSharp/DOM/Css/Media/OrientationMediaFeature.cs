namespace AngleSharp.DOM.Css.Media
{
    using AngleSharp.Css;
    using System;

    sealed class OrientationMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<Boolean> Converter = CSSProperty.Toggle(Keywords.Portrait, Keywords.Landscape);
        Boolean _portrait;

        #endregion

        #region ctor

        public OrientationMediaFeature()
            : base(FeatureNames.Orientation)
        {
            _portrait = false;
        }

        #endregion

        #region Properties

        public Boolean IsLandscape
        {
            get { return !_portrait; }
        }

        public Boolean IsPortrait
        {
            get { return _portrait; }
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            _portrait = false;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converter.TryConvert(value, m => _portrait = m);
        }

        public override Boolean Validate(IWindow window)
        {
            return true;
        }

        #endregion
    }
}
