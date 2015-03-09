namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-style
    /// </summary>
    sealed class CssBorderTopStyleProperty : CssProperty
    {
        #region ctor

        internal CssBorderTopStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderTopStyle, rule)
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

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.LineStyleConverter.Validate(value);
        }

        #endregion
    }
}
