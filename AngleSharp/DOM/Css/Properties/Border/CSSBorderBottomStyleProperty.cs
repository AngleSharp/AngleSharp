namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-style
    /// </summary>
    sealed class CSSBorderBottomStyleProperty : CSSBorderPartStyleProperty, ICssBorderStyleProperty
    {
        #region ctor

        internal CSSBorderBottomStyleProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderBottomStyle, rule)
        { 
        }

        #endregion
    }
}
