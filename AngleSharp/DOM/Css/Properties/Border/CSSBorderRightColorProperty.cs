namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-color
    /// </summary>
    sealed class CSSBorderRightColorProperty : CSSBorderPartColorProperty, ICssBorderColorProperty
    {
        #region ctor

        internal CSSBorderRightColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderRightColor, rule)
        { 
        }

        #endregion
    }
}
