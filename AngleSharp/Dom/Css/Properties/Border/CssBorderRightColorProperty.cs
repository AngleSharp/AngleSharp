namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

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
            // Default: Color.Transparent
            get { return Converters.CurrentColorConverter; }
        }

        #endregion
    }
}
