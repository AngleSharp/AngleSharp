namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/line-height
    /// </summary>
    sealed class CssLineHeightProperty : CssProperty
    {
        #region ctor

        internal CssLineHeightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.LineHeight, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return new Length(120f, Length.Unit.Percent);
        }

        protected override Object Compute(IElement element)
        {
            return Converters.LineHeightConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.LineHeightConverter.Validate(value);
        }

        #endregion
    }
}
