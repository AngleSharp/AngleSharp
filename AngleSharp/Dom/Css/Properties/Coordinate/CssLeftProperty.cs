namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/left
    /// </summary>
    sealed class CssLeftProperty : CssCoordinateProperty
    {
        #region ctor

        internal CssLeftProperty(CssStyleDeclaration rule)
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
