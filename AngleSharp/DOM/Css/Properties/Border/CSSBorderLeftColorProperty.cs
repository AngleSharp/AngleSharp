namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-color
    /// </summary>
    public sealed class CSSBorderLeftColorProperty : CSSBorderPartColorProperty
    {
        #region ctor

        internal CSSBorderLeftColorProperty()
            : base(PropertyNames.BorderLeftColor)
        { }

        #endregion
    }
}
