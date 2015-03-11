namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available:
    /// http://dev.w3.org/csswg/css-images-3/#the-object-fit
    /// </summary>
    sealed class CssObjectFitProperty : CssProperty
    {
        #region ctor

        internal CssObjectFitProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ObjectFit, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return ObjectFitting.Fill;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.ObjectFittingConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.ObjectFittingConverter.Validate(value);
        }

        #endregion
    }
}
