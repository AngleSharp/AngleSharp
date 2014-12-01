namespace AngleSharp.Css
{
    using AngleSharp.DOM.Css;
    using AngleSharp.Extensions;
    using System;

    struct Repeat : ICssValue
    {
        public BackgroundRepeat Horizontal;
        public BackgroundRepeat Vertical;

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get { return String.Format("{0} {1}", Map.BackgroundRepeats.GetIdentifier(Horizontal), Map.BackgroundRepeats.GetIdentifier(Vertical)); }
        }

        #endregion
    }
}
