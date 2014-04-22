namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-style
    /// </summary>
    public sealed class CSSBorderLeftStyleProperty : CSSBorderPartStyleProperty
    {
        #region ctor

        internal CSSBorderLeftStyleProperty()
            : base(PropertyNames.BorderLeftStyle)
        { }

        #endregion
    }
}
