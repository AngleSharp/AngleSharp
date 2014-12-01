namespace AngleSharp.DOM.Css
{
    using System;

    struct BackgroundSize : ICssValue
    {
        public Boolean IsCovered;
        public Boolean IsContained;
        public IDistance Width;
        public IDistance Height;

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get { return String.Empty; }//TODO
        }

        #endregion
    }
}
