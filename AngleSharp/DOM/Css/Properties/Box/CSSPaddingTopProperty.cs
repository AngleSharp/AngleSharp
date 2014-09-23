namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-top
    /// </summary>
    sealed class CSSPaddingTopProperty : CSSPaddingPartProperty, ICssPaddingTopProperty
    {
        #region ctor

        internal CSSPaddingTopProperty()
            : base(PropertyNames.PaddingTop)
        {
        }

        #endregion

        #region Property

        public IDistance Top
        {
            get { return Padding; }
        }

        #endregion
    }
}
