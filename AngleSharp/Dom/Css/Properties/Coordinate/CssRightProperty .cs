namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/right
    /// </summary>
    sealed class CssRightProperty : CssCoordinateProperty
    {
        #region ctor

        internal CssRightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Right, rule)
        {
        }

        #endregion

        #region Property

        public Length? Right
        {
            get { return Position; }
        }

        #endregion
    }
}
