namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/white-space
    /// Gets the selected whitespace handling mode.
    /// </summary>
    sealed class CssWhiteSpaceProperty : CssProperty
    {
        #region ctor

        internal CssWhiteSpaceProperty()
            : base(PropertyNames.WhiteSpace, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Whitespace.Normal
            get { return Converters.WhitespaceConverter; }
        }

        #endregion
    }
}
