namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information can be found:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/clip
    /// Gets the shape of the selected clipping region. If this value is
    /// null, then the clipping is determined automatically.
    /// </summary>
    sealed class CssClipProperty : CssProperty
    {
        #region ctor

        internal CssClipProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Clip, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.ShapeConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.ShapeConverter.Validate(value);
        }

        #endregion
    }
}
