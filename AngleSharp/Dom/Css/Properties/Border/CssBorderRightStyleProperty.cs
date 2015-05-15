namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-style
    /// </summary>
    sealed class CssBorderRightStyleProperty : CssProperty
    {
        #region ctor

        internal CssBorderRightStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderRightStyle, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return LineStyle.None;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.LineStyleConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.LineStyleConverter.Validate(value);
        }

        #endregion
    }
}
