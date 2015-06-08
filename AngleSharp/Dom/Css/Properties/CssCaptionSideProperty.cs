namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/caption-side
    /// Gets if the caption box will be above the table.
    /// Otherwise the caption box will be below the table.
    /// </summary>
    sealed class CssCaptionSideProperty : CssProperty
    {
        #region ctor

        internal CssCaptionSideProperty()
            : base(PropertyNames.CaptionSide)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.CaptionSideConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return true;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.CaptionSideConverter.Validate(value);
        }

        #endregion
    }
}
