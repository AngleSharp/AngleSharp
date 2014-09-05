namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-color
    /// </summary>
    sealed class CSSBorderBottomColorProperty : CSSBorderPartColorProperty, ICssBorderColorProperty
    {
        #region ctor

        internal CSSBorderBottomColorProperty()
            : base(PropertyNames.BorderBottomColor)
        { }

        #endregion
    }
}
