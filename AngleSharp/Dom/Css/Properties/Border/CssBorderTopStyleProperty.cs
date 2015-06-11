namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-style
    /// </summary>
    sealed class CssBorderTopStyleProperty : CssProperty
    {
        #region ctor

        internal CssBorderTopStyleProperty()
            : base(PropertyNames.BorderTopStyle)
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
