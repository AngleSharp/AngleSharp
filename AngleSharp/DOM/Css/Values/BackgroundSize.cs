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
            get { return CssValueType.List; }
        }

        String ICssValue.CssText
        {
            get
            {
                if (IsCovered)
                    return Keywords.Cover;
                else if (IsContained)
                    return Keywords.Contain;

                return String.Format("{0} {1}", Width.CssText, Height.CssText);
            }
        }

        #endregion
    }
}
