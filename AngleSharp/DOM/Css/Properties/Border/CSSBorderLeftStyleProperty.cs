namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-style
    /// </summary>
    sealed class CSSBorderLeftStyleProperty : CSSBorderPartStyleProperty, ICssBorderStyleProperty
    {
        #region ctor

        internal CSSBorderLeftStyleProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderLeftStyle, rule)
        {
        }

        #endregion
    }
}
