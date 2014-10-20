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

        internal CSSBorderRightStyleProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderRightStyle, rule)
        {
        }

        #endregion
    }
}
