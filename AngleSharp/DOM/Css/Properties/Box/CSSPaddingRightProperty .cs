namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-right
    /// </summary>
    sealed class CSSPaddingRightProperty : CSSPaddingPartProperty, ICssPaddingRightProperty
    {
        #region ctor

        internal CSSPaddingRightProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.PaddingRight, rule)
        {
        }

        #endregion

        #region Property

        public IDistance Right
        {
            get { return Padding; }
        }

        #endregion
    }
}
