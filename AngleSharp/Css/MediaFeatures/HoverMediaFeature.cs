namespace AngleSharp.Css.MediaFeatures
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class HoverMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<HoverAbility> Converter = Map.HoverAbilities.ToConverter();
        HoverAbility _hover;

        #endregion

        #region ctor

        public HoverMediaFeature()
            : base(FeatureNames.Hover)
        {
            _hover = HoverAbility.Hover;
        }

        #endregion

        #region Properties

        public HoverAbility Hover
        {
            get { return _hover; }
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            _hover = HoverAbility.Hover;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converter.TryConvert(value, m => _hover = m);
        }

        public override Boolean Validate(RenderDevice device)
        {
            var desired = _hover;
            //Nothing yet, so we assume we have a headless browser
            return desired == HoverAbility.None;
        }

        #endregion
    }
}
