namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-color
    /// </summary>
    sealed class CSSBorderBottomColorProperty : CSSBorderPartColorProperty, ICssBorderColorProperty
    {
        #region ctor

        internal CSSBorderBottomColorProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderBottomColor, rule)
        {
        }

        #endregion
    }
}
