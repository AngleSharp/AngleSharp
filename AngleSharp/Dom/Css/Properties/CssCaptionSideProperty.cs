namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/caption-side
    /// Gets if the caption box will be above the table.
    /// Otherwise the caption box will be below the table.
    /// </summary>
    sealed class CssCaptionSideProperty : CssProperty
    {
        #region ctor

        internal CssCaptionSideProperty(CssStyleDeclaration rule)
            : base(PropertyNames.CaptionSide, rule)
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
            return Converters.CaptionSideConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.CaptionSideConverter.Validate(value);
        }

        #endregion
    }
}
