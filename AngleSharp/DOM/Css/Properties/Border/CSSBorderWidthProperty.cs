namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-width
    /// </summary>
    sealed class CssBorderWidthProperty : CssShorthandProperty, ICssBorderWidthsProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>> Converter = 
            CssBorderPartWidthProperty.Converter.Val().Periodic();
        readonly CssBorderTopWidthProperty _top;
        readonly CssBorderRightWidthProperty _right;
        readonly CssBorderBottomWidthProperty _bottom;
        readonly CssBorderLeftWidthProperty _left;

        #endregion

        #region ctor

        internal CssBorderWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderWidth, rule, PropertyFlags.Animatable)
        {
            _top = Get<CssBorderTopWidthProperty>();
            _right = Get<CssBorderRightWidthProperty>();
            _bottom = Get<CssBorderBottomWidthProperty>();
            _left = Get<CssBorderLeftWidthProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the width of the top border.
        /// </summary>
        public Length Top
        {
            get { return _top.Width; }
        }

        /// <summary>
        /// Gets the value for the width of the right border.
        /// </summary>
        public Length Right
        {
            get { return _right.Width; }
        }

        /// <summary>
        /// Gets the value for the width of the bottom border.
        /// </summary>
        public Length Bottom
        {
            get { return _bottom.Width; }
        }

        /// <summary>
        /// Gets the value for the width of the left border.
        /// </summary>
        public Length Left
        {
            get { return _left.Width; }
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
