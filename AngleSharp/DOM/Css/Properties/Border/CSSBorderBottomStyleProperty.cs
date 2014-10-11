namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-style
    /// </summary>
    sealed class CSSBorderBottomStyleProperty : CSSBorderPartStyleProperty, ICssBorderStyleProperty
    {
        #region ctor

        internal CSSBorderBottomStyleProperty()
            : base(PropertyNames.BorderBottomStyle)
        { }

        #endregion
    }
}
