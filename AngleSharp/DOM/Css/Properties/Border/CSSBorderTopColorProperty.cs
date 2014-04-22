namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-color
    /// </summary>
    public sealed class CSSBorderTopColorProperty : CSSBorderPartColorProperty
    {
        #region ctor

        internal CSSBorderTopColorProperty()
            : base(PropertyNames.BorderTopColor)
        { }

        #endregion
    }
}
