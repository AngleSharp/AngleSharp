namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule
    /// </summary>
    sealed class CSSColumnRuleProperty : CSSShorthandProperty, ICssColumnRuleProperty
    {
        #region Fields

        readonly CSSColumnRuleColorProperty _color;
        readonly CSSColumnRuleStyleProperty _style;
        readonly CSSColumnRuleWidthProperty _width;

        #endregion

        #region ctor

        internal CSSColumnRuleProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.ColumnRule, rule, PropertyFlags.Animatable)
        {
            _color = Get<CSSColumnRuleColorProperty>();
            _style = Get<CSSColumnRuleStyleProperty>();
            _width = Get<CSSColumnRuleWidthProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the column-rule color.
        /// </summary>
        public Color Color
        {
            get { return _color.Color; }
        }

        /// <summary>
        /// Gets the value of the column-rule style.
        /// </summary>
        public LineStyle Style
        {
            get { return _style.Style; }
        }

        /// <summary>
        /// Gets the value of the column-rule width.
        /// </summary>
        public Length Width
        {
            get { return _width.Width; }
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
            CSSValue color = null;
            CSSValue width = null;
            CSSValue style = null;

            if (list.Length > 3)
                return false;

            for (int i = 0; i < list.Length; i++)
            {
                if (!_color.CanStore(list[i], ref color) && 
                    !_width.CanStore(list[i], ref width) && 
                    !_style.CanStore(list[i], ref style))
                    return false;
            }

            return _color.TrySetValue(color) && _width.TrySetValue(width) && _style.TrySetValue(style);
        }

        #endregion
    }
}
