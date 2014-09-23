namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/left
    /// </summary>
    sealed class CSSLeftProperty : CSSCoordinateProperty, ICssLeftProperty
    {
        #region ctor

        internal CSSLeftProperty()
            : base(PropertyNames.Left)
        {
        }

        #endregion

        #region Property

        public IDistance Left
        {
            get { return Position; }
        }

        #endregion
    }
}
