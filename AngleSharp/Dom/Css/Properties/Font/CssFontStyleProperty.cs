namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-style
    /// Gets the selected font style.
    /// </summary>
    sealed class CssFontStyleProperty : CssProperty
    {
        #region ctor

        internal CssFontStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.FontStyle, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return FontStyle.Normal;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.FontStyleConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.FontStyleConverter.Validate(value);
        }

        #endregion
    }
}
