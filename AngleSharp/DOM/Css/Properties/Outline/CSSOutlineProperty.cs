namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline
    /// </summary>
    sealed class CSSOutlineProperty : CSSShorthandProperty, ICssOutlineProperty
    {
        #region Fields

        readonly CSSOutlineStyleProperty _style;
        readonly CSSOutlineWidthProperty _width;
        readonly CSSOutlineColorProperty _color;

        #endregion

        #region ctor

        internal CSSOutlineProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Outline, rule, PropertyFlags.Animatable)
        {
            _style = Get<CSSOutlineStyleProperty>();
            _width = Get<CSSOutlineWidthProperty>();
            _color = Get<CSSOutlineColorProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected outline style property.
        /// </summary>
        public LineStyle Style
        {
            get { return _style.Style; }
        }

        /// <summary>
        /// Gets the selected outline width property.
        /// </summary>
        public Length Width
        {
            get { return _width.Width; }
        }

        /// <summary>
        /// Gets the selected outline color property.
        /// </summary>
        public Color Color
        {
            get { return _color.Color; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var list = value as CSSValueList ?? new CSSValueList(value);
            CSSValue style = null;
            CSSValue width = null;
            CSSValue color = null;

            if (list.Length > 3)
                return false;

            for (int i = 0; i < list.Length; i++)
            {
                if (!_style.CanStore(list[i], ref style) &&
                    !_width.CanStore(list[i], ref width) &&
                    !_color.CanStore(list[i], ref color))
                    return false;
            }

            return _style.TrySetValue(style) && _width.TrySetValue(width) && _color.TrySetValue(color);
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return String.Format("{0} {1} {2}", _width.SerializeValue(), _color.SerializeValue(), _style.SerializeValue());
        }

        #endregion
    }
}
