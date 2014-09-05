namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-color
    /// </summary>
    sealed class CSSBorderTopColorProperty : CSSBorderPartColorProperty, ICssBorderColorProperty
    {
        #region ctor

        internal CSSBorderTopColorProperty()
            : base(PropertyNames.BorderTopColor)
        { }

        #endregion
    }
}
