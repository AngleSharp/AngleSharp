namespace AngleSharp.Css.MediaFeatures
{
    using System;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;

    sealed class HoverMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter TheConverter = Map.HoverAbilities.ToConverter();

        #endregion

        #region ctor

        public HoverMediaFeature()
            : base(FeatureNames.Hover)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            // Default: HoverAbility.Hover
            get { return TheConverter; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var hover = HoverAbility.Hover;
            var desired = hover;
            //Nothing yet, so we assume we have a headless browser
            return desired == HoverAbility.None;
        }

        #endregion
    }
}
