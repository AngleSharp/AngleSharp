namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/left
    /// </summary>
    sealed class CSSLeftProperty : CSSCoordinateProperty, ICssLeftProperty
    {
        #region ctor

        internal CSSLeftProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Left, rule)
        {
        }

        #endregion

        #region Property

        public Length? Left
        {
            get { return Position; }
        }

        #endregion
    }
}
