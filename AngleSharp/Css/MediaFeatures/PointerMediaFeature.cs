namespace AngleSharp.Css.MediaFeatures
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class PointerMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<PointerAccuracy> Converter = Map.PointerAccuracies.ToConverter();
        PointerAccuracy _accuracy;

        #endregion

        #region ctor

        public PointerMediaFeature()
            : base(FeatureNames.Pointer)
        {
            _accuracy = PointerAccuracy.Fine;
        }

        #endregion

        #region Properties

        public PointerAccuracy Accuracy
        {
            get { return _accuracy; }
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            _accuracy = PointerAccuracy.Fine;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converter.TryConvert(value, m => _accuracy = m);
        }

        public override Boolean Validate(RenderDevice device)
        {
            var desired = _accuracy;
            //Nothing yet, so we assume we have a headless browser
            return desired == PointerAccuracy.None;
        }

        #endregion
    }
}
