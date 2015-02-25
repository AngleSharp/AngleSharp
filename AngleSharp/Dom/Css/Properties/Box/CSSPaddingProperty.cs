namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding
    /// </summary>
    sealed class CssPaddingProperty : CssShorthandProperty, ICssPaddingProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>> Converter = 
            CssPaddingPartProperty.Converter.Val().Periodic();

        readonly CssPaddingTopProperty _top;
        readonly CssPaddingRightProperty _right;
        readonly CssPaddingBottomProperty _bottom;
        readonly CssPaddingLeftProperty _left;

        #endregion

        #region ctor

        internal CssPaddingProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Padding, rule)
        {
            _top = Get<CssPaddingTopProperty>();
            _right = Get<CssPaddingRightProperty>();
            _bottom = Get<CssPaddingBottomProperty>();
            _left = Get<CssPaddingLeftProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the top padding.
        /// </summary>
        public Length Top
        {
            get { return _top.Top; }
        }

        /// <summary>
        /// Gets the value for the right padding.
        /// </summary>
        public Length Right
        {
            get { return _right.Right; }
        }

        /// <summary>
        /// Gets the value for the bottom padding.
        /// </summary>
        public Length Bottom
        {
            get { return _bottom.Bottom; }
        }

        /// <summary>
        /// Gets the value for the left padding.
        /// </summary>
        public Length Left
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
