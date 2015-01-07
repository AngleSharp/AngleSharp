namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-style
    /// </summary>
    sealed class CssBorderLeftStyleProperty : CssBorderPartStyleProperty, ICssBorderStyleProperty
    {
        #region ctor

        internal CssBorderLeftStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderLeftStyle, rule)
        {
        }

        #endregion
    }
}
