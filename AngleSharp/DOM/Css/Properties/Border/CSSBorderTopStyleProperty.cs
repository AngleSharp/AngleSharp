namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-style
    /// </summary>
    sealed class CSSBorderTopStyleProperty : CSSBorderPartStyleProperty, ICssBorderStyleProperty
    {
        #region ctor

        internal CSSBorderTopStyleProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderTopStyle, rule)
        { 
        }

        #endregion
    }
}
