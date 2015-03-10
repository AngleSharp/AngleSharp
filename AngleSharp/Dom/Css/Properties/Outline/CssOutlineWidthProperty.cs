namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-width
    /// Gets the width of the outline of an element. An outline is a line
    /// that is drawn around elements, outside the border edge, to make the
    /// element stand out.
    /// </summary>
    sealed class CssOutlineWidthProperty : CssProperty
    {
        #region ctor

        internal CssOutlineWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.OutlineWidth, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Medium;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.LineWidthConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.LineWidthConverter.Validate(value);
        }

        #endregion
    }
}
