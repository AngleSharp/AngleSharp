namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information can be found:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/clip
    /// Gets the shape of the selected clipping region. If this value is
    /// null, then the clipping is determined automatically.
    /// </summary>
    sealed class CssClipProperty : CssProperty
    {
        #region ctor

        internal CssClipProperty()
            : base(PropertyNames.Clip, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.ShapeConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        #endregion
    }
}
