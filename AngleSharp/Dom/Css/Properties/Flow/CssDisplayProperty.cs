namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/display
    /// Gets the value of the display mode.
    /// </summary>
    sealed class CssDisplayProperty : CssProperty
    {
        #region ctor

        internal CssDisplayProperty()
            : base(PropertyNames.Display)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.DisplayModeConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return DisplayMode.Inline;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.DisplayModeConverter.Validate(value);
        }

        #endregion
    }
}
