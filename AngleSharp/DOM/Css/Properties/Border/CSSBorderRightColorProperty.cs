namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-color
    /// </summary>
    sealed class CSSBorderRightColorProperty : CSSBorderPartColorProperty, ICssBorderColorProperty
    {
        #region ctor

        internal CSSBorderRightColorProperty()
            : base(PropertyNames.BorderRightColor)
        { }

        #endregion
    }
}
