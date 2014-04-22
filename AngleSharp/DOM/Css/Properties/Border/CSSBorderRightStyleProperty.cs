namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-style
    /// </summary>
    public sealed class CSSBorderRightStyleProperty : CSSBorderPartColorProperty
    {
        #region ctor

        internal CSSBorderRightStyleProperty()
            : base(PropertyNames.BorderRightStyle)
        { }

        #endregion
    }
}
