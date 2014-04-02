namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font
    /// </summary>
    sealed class CSSFontProperty : CSSProperty
    {
        #region Fields

        CSSFontStyleProperty _style;
        CSSFontVariantProperty _variant;
        CSSFontWeightProperty _weight;
        CSSFontSizeProperty _size;
        CSSFontFamilyProperty _family;
        CSSLineHeightProperty _height;

        #endregion

        #region ctor

        public CSSFontProperty()
            : base(PropertyNames.FONT)
        {
            //TODO: These will be only created IF they have been set.
            //_style = new Style();
            //_variant = new Variant();
            //_weight = new Weight();
            //_size = new Size();
            //_family = new Family();
            //_height = new LineHeight();
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            //TODO
            return base.IsValid(value);
        }

        #endregion
    }
}
