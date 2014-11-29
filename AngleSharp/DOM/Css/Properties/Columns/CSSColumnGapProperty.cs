namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-gap
    /// </summary>
    sealed class CSSColumnGapProperty : CSSProperty, ICssColumnGapProperty
    {
        #region Fields

        static readonly Length _normal = new Length(1f, Length.Unit.Em);

        /// <summary>
        /// Defines the size of the gap between columns. It must not
        /// be negative, but may be equal to 0.
        /// </summary>
        Length _gap;

        #endregion

        #region ctor

        internal CSSColumnGapProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.ColumnGap, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected width of gaps between columns.
        /// </summary>
        public Length Gap
        {
            get { return _gap; }
        }

        #endregion

        #region Methods

        public void SetGap(Length gap)
        {
            _gap = gap;
        }

        internal override void Reset()
        {
            _gap = _normal;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return WithLength().Or(TakeOne(Keywords.Normal, _normal)).TryConvert(value, SetGap);
        }

        #endregion
    }
}
