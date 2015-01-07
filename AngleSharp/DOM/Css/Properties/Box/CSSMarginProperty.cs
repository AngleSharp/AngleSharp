namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin
    /// </summary>
    sealed class CssMarginProperty : CssShorthandProperty, ICssMarginProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>> Converter = 
            CssMarginPartProperty.Converter.Val().Periodic();
        readonly CssMarginTopProperty _top;
        readonly CssMarginRightProperty _right;
        readonly CssMarginBottomProperty _bottom;
        readonly CssMarginLeftProperty _left;

        #endregion

        #region ctor

        internal CssMarginProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Margin, rule)
        {
            _top = Get<CssMarginTopProperty>();
            _right = Get<CssMarginRightProperty>();
            _bottom = Get<CssMarginBottomProperty>();
            _left = Get<CssMarginLeftProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the top margin.
        /// </summary>
        public Length? Top
        {
            get { return _top.Top; }
        }

        /// <summary>
        /// Gets the value for the right margin.
        /// </summary>
        public Length? Right
        {
            get { return _right.Right; }
        }

        /// <summary>
        /// Gets the value for the bottom margin.
        /// </summary>
        public Length? Bottom
        {
            get { return _bottom.Bottom; }
        }

        /// <summary>
        /// Gets the value for the left margin.
        /// </summary>
        public Length? Left
        {
            get { return _left.Left; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                _top.TrySetValue(m.Item1);
                _right.TrySetValue(m.Item2);
                _bottom.TrySetValue(m.Item3);
                _left.TrySetValue(m.Item4);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return SerializePeriodic(_top, _right, _bottom, _left);
        }

        #endregion
    }
}
