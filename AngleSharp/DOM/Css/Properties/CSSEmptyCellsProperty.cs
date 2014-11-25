namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/empty-cells
    /// </summary>
    sealed class CSSEmptyCellsProperty : CSSProperty, ICssEmptyCellsProperty
    {
        #region Fields

        Boolean _visible;

        #endregion

        #region ctor

        internal CSSEmptyCellsProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.EmptyCells, rule, PropertyFlags.Inherited)
        {
            Reset();
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

        public void SetVisible(Boolean visible)
        {
            _visible = visible;
        }

        internal override void Reset()
        {
            _visible = true;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.TakeOne(Keywords.Show, true).Or(this.TakeOne(Keywords.Hide, false)).TryConvert(value, SetVisible);
        }

        #endregion
    }
}
