namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en/docs/Web/CSS/@font-face
    /// </summary>
    sealed class CssUnicodeRangeProperty : CssProperty
    {
        #region ctor

        public CssUnicodeRangeProperty()
            : base(PropertyNames.UnicodeRange)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.Any; }
        }

        #endregion

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return true;
        }
    }
}
