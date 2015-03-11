namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/white-space
    /// Gets the selected whitespace handling mode.
    /// </summary>
    sealed class CssWhiteSpaceProperty : CssProperty
    {
        #region ctor

        internal CssWhiteSpaceProperty(CssStyleDeclaration rule)
            : base(PropertyNames.WhiteSpace, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Whitespace.Normal;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.WhitespaceConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.WhitespaceConverter.Validate(value);
        }

        #endregion
    }
}
