namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-style
    /// </summary>
    public sealed class CSSBorderBottomStyleProperty : CSSBorderPartStyleProperty
    {
        #region ctor

        internal CSSBorderBottomStyleProperty()
            : base(PropertyNames.BorderBottomStyle)
        { }

        #endregion
    }
}
