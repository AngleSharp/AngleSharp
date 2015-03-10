namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/display
    /// Gets the value of the display mode.
    /// </summary>
    sealed class CssDisplayProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<DisplayMode> Converter = 
            Map.DisplayModes.ToConverter();

        #endregion

        #region ctor

        internal CssDisplayProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Display, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return DisplayMode.Inline;
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
