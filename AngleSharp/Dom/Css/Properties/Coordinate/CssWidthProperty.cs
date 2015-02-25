namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/width
    /// </summary>
    sealed class CssWidthProperty : CssCoordinateProperty, ICssWidthProperty
    {
        #region ctor

        internal CssWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Width, rule)
        {
        }

        #endregion

        #region Property

        public Length? Width
        {
            get { return Position; }
        }

        #endregion
    }
}
