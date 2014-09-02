namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-bottom
    /// </summary>
    sealed class CSSPaddingBottomProperty : CSSPaddingPartProperty, ICssPaddingBottomProperty
    {
        #region ctor

        internal CSSPaddingBottomProperty()
            : base(PropertyNames.PaddingBottom)
        {
        }

        #endregion
    }
}
