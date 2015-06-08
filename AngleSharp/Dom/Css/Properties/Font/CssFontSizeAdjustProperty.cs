namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// http://dev.w3.org/csswg/css-fonts/#propdef-font-size-adjust
    /// Gets the aspect value specified by the property, if any.
    /// </summary>
    sealed class CssFontSizeAdjustProperty : CssProperty
    {
        #region ctor

        internal CssFontSizeAdjustProperty()
            : base(PropertyNames.FontSizeAdjust, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.OptionalNumberConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.OptionalNumberConverter.Validate(value);
        }

        #endregion
    }
}
