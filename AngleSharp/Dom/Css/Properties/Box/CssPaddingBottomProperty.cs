namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-bottom
    /// </summary>
    sealed class CssPaddingBottomProperty : CssPaddingPartProperty
    {
        #region ctor

        internal CssPaddingBottomProperty(CssStyleDeclaration rule)
            : base(PropertyNames.PaddingBottom, rule)
        {
        }

        #endregion

        #region Property

        public Length Bottom
        {
            get { return Padding; }
        }

        #endregion
    }
}
