namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-color
    /// </summary>
    sealed class CssBorderRightColorProperty : CssProperty
    {
        #region ctor

        internal CssBorderRightColorProperty()
            : base(PropertyNames.BorderRightColor)
        { 
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.CurrentColorConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Color.Transparent;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.CurrentColorConverter.Validate(value);
        }

        #endregion
    }
}
