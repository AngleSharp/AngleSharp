namespace AngleSharp.DOM.Css
{
    using System;

    struct FontWeight : ICssValue
    {
        public Boolean IsRelative;
        public Int32 Value;

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
