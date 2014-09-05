namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-style
    /// </summary>
    sealed class CSSBorderTopStyleProperty : CSSBorderPartStyleProperty, ICssBorderStyleProperty
    {
        #region ctor

        internal CSSBorderTopStyleProperty()
            : base(PropertyNames.BorderTopStyle)
        { }

        #endregion
    }
}
