namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/top
    /// </summary>
    sealed class CssTopProperty : CssCoordinateProperty
    {
        #region ctor

        internal CssTopProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Top, rule)
        {
        }

        #endregion

        #region Property

        public Length? Top
        {
            get { return Position; }
        }

        #endregion
    }
}
