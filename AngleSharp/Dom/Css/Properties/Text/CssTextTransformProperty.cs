namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-transform
    /// Gets the selected text transformation mode.
    /// </summary>
    sealed class CssTextTransformProperty : CssProperty
    {
        #region ctor

        internal CssTextTransformProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TextTransform, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return TextTransform.None;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.TextTransformConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.TextTransformConverter.Validate(value);
        }

        #endregion
    }
}
