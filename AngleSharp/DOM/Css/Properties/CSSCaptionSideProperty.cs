namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/caption-side
    /// </summary>
    sealed class CSSCaptionSideProperty : CSSProperty, ICssCaptionSideProperty
    {
        #region Fields

        Boolean _top;

        #endregion

        #region ctor

        internal CSSCaptionSideProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.CaptionSide, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the caption box will be above the table.
        /// Otherwise the caption box will be below the table.
        /// </summary>
        public Boolean IsOnTop
        {
            get { return _top; }
        }

        #endregion

        #region Methods

        public void SetMode(Boolean onTop)
        {
            _top = onTop;
        }

        internal override void Reset()
        {
            _top = true;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.TakeOne(Keywords.Top, true).Or(this.TakeOne(Keywords.Bottom, false)).TryConvert(value, SetMode);
        }

        #endregion
    }
}
