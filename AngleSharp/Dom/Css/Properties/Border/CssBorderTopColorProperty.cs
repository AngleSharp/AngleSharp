namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-color
    /// </summary>
    sealed class CssBorderTopColorProperty : CssProperty
    {
        #region ctor

        internal CssBorderTopColorProperty()
            : base(PropertyNames.BorderTopColor)
        { 
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Color.Transparent
            get { return Converters.CurrentColorConverter; }
        }

        #endregion
    }
}
