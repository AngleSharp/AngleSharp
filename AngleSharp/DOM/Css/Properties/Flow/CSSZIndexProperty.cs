namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/z-index
    /// </summary>
    sealed class CSSZIndexProperty : CSSProperty, ICssZIndexProperty
    {
        #region Fields

        Int32? _index;

        #endregion

        #region ctor

        internal CSSZIndexProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.ZIndex, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the index in the stacking order, if any.
        /// </summary>
        public Int32? Index
        {
            get { return _index; }
        }

        public void SetIndex(Int32? index)
        {
            _index = index;
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _index = null;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.WithInteger().To(m => new Int32?(m)).Or(this.TakeOne(Keywords.Auto, (Int32?)null)).TryConvert(value, SetIndex);
        }

        #endregion
    }
}
