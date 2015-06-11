namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-style
    /// </summary>
    sealed class CssBorderBottomStyleProperty : CssProperty
    {
        #region ctor

        internal CssBorderBottomStyleProperty()
            : base(PropertyNames.BorderBottomStyle)
        { 
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.LineStyleConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return LineStyle.None;
        }

        #endregion
    }
}
