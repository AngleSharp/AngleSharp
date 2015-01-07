namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/height
    /// </summary>
    sealed class CssHeightProperty : CssCoordinateProperty, ICssHeightProperty
    {
        #region ctor

        internal CssHeightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Height, rule)
        {
        }

        #endregion

        #region Property

        public Length? Height
        {
            get { return Position; }
        }

        #endregion
    }
}
