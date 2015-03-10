namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-type
    /// Gets the selected style for the list.
    /// </summary>
    sealed class CssListStyleTypeProperty : CssProperty
    {
        #region ctor

        internal CssListStyleTypeProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ListStyleType, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return ListStyle.Disc;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.ListStyleConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.ListStyleConverter.Validate(value);
        }

        #endregion
    }
}
