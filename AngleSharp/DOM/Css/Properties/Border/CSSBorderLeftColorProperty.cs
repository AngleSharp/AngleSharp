namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-color
    /// </summary>
    sealed class CSSBorderLeftColorProperty : CSSBorderPartColorProperty, ICssBorderColorProperty
    {
        #region ctor

        internal CSSBorderLeftColorProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderLeftColor, rule)
        { 
        }

        #endregion
    }
}
