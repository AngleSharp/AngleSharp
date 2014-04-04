namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-right
    /// </summary>
    sealed class CSSMarginRightProperty : CSSMarginPartProperty
    {
        #region ctor

        public CSSMarginRightProperty()
            : base(PropertyNames.MarginRight)
        {
        }

        #endregion
    }
}
