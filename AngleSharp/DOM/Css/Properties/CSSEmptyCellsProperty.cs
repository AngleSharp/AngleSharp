namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/empty-cells
    /// </summary>
    public sealed class CSSEmptyCellsProperty : CSSProperty
    {
        #region Fields

        Boolean _visible;

        #endregion

        #region ctor

        internal CSSEmptyCellsProperty()
            : base(PropertyNames.EmptyCells)
        {
            _visible = true;
            _inherited = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if borders and backgrounds should be drawn like
        /// in a normal cells. Otherwise no border or backgrounds
        /// should be drawn.
        /// </summary>
        public Boolean IsVisible
        {
            get { return _visible; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("show"))
                _visible = true;
            else if (value.Is("hide"))
                _visible = false;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
