namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-top
    /// </summary>
    sealed class CSSMarginTopProperty : CSSMarginPartProperty
    {
        #region ctor

        public CSSMarginTopProperty()
            : base(PropertyNames.MarginTop)
        {
        }

        #endregion
    }
}
