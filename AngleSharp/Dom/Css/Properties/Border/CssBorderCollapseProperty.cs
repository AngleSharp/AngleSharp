namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-collapse
    /// </summary>
    sealed class CssBorderCollapseProperty : CssProperty
    {
        #region ctor

        internal CssBorderCollapseProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderCollapse, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return true;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.BorderCollapseConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.BorderCollapseConverter.Validate(value);
        }

        #endregion
    }
}
