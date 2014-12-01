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
            get
            {
                if (IsRelative)
                    return Value == 100 ? Keywords.Bolder : Keywords.Lighter;

                if (Value == 400)
                    return Keywords.Normal;
                else if (Value == 700)
                    return Keywords.Bold;

                return Value.ToString();
            }
        }

        #endregion
    }
}
