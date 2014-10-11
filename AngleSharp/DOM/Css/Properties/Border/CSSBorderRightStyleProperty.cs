namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-style
    /// </summary>
    sealed class CSSBorderRightStyleProperty : CSSBorderPartStyleProperty, ICssBorderStyleProperty
    {
        #region ctor

        internal CSSBorderRightStyleProperty()
            : base(PropertyNames.BorderRightStyle)
        { }

        #endregion
    }
}
