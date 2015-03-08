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
        #region Fields

        static readonly IValueConverter<ObjectFitting> Converter = 
            Map.ObjectFittings.ToConverter();

        #endregion

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
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}
