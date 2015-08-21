namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an wrapper property for media feature instances.
    /// </summary>
    sealed class CssFeatureProperty : CssProperty
    {
        #region Fields

        readonly MediaFeature _feature;

        #endregion

        #region ctor

        internal CssFeatureProperty(MediaFeature feature)
            : base(feature.Name)
        {
            _feature = feature;
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return _feature.Converter; }
        }

        internal MediaFeature Feature
        {
            get { return _feature; }
        }

        #endregion

        #region Methods

        public override IEnumerable<CssNode> GetChildren()
        {
            yield return _feature;
        }

        #endregion
    }
}
