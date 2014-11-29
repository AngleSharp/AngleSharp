namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/backface-visibility
    /// </summary>
    sealed class CSSBackfaceVisibilityProperty : CSSProperty, ICssBackfaceVisibilityProperty
    {
        #region Fields

        Boolean _visible;

        #endregion

        #region ctor

        internal CSSBackfaceVisibilityProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BackfaceVisibility, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the back face is visible, allowing the front
        /// face to be displayed mirrored. Otherwise the back face
        /// is not visible, hiding the front face.
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
            return Toggle(Keywords.Visible, Keywords.Hidden).TryConvert(value, SetVisible);
        }

        #endregion
    }
}
