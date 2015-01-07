namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-top
    /// </summary>
    sealed class CssPaddingTopProperty : CssPaddingPartProperty, ICssPaddingTopProperty
    {
        #region ctor

        internal CssPaddingTopProperty(CssStyleDeclaration rule)
            : base(PropertyNames.PaddingTop, rule)
        {
        }

        #endregion

        #region Property

        public Length Top
        {
            get { return Padding; }
        }

        #endregion
    }
}
