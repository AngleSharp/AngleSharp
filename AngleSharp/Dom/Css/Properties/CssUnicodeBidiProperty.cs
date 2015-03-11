namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/unicode-bidi
    /// </summary>
    sealed class CssUnicodeBidiProperty : CssProperty
    {
        #region ctor

        internal CssUnicodeBidiProperty(CssStyleDeclaration rule)
            : base(PropertyNames.UnicodeBidi, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return UnicodeMode.Normal;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.UnicodeModeConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.UnicodeModeConverter.Validate(value);
        }

        #endregion
    }
}
