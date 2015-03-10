namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-align
    /// Gets the selected horizontal alignment mode.
    /// </summary>
    sealed class CssTextAlignProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<HorizontalAlignment> Converter =
            Map.HorizontalAlignments.ToConverter();

        #endregion

        #region ctor

        internal CssTextAlignProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TextAlign, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return HorizontalAlignment.Left;
        }

        protected override Object Compute(IElement element)
        {
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}
