namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-after
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssPageBreakAfterProperty : CssProperty
    {
        #region ctor

        internal CssPageBreakAfterProperty(CssStyleDeclaration rule)
            : base(PropertyNames.PageBreakAfter, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return BreakMode.Auto;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.PageBreakModeConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.PageBreakModeConverter.Validate(value);
        }

        #endregion
    }
}
