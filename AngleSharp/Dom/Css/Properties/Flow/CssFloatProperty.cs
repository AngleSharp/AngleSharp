namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/float
    /// Gets the value of the floating property.
    /// </summary>
    sealed class CssFloatProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Floating> Converter = 
            Map.FloatingModes.ToConverter();

        #endregion

        #region ctor

        internal CssFloatProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Float, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Floating.None;
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
