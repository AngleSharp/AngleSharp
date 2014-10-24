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

        Length _width;
        LineStyle _style;
        Color _color;

        #endregion

        #region ctor

        internal CSSColumnRuleProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.ColumnRule, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the column-rule color.
        /// </summary>
        public Color Color
        {
            get { return _color; }
        }

        /// <summary>
        /// Gets the value of the column-rule style.
        /// </summary>
        public LineStyle Style
        {
            get { return _style; }
        }

        /// <summary>
        /// Gets the value of the column-rule width.
        /// </summary>
        public Length Width
        {
            get { return _width; }
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
            Color? color = null;
            Length? width = null;
            LineStyle? style = null;

            if (list.Length > 3)
                return false;

            for (int i = 0; i < list.Length; i++)
            {
                if (!color.HasValue && (color = list[i].ToColor()).HasValue)
                    continue;
                else if (!width.HasValue && (width = list[i].ToBorderWidth()).HasValue)
                    continue;
                else if (!style.HasValue && (style = list[i].ToLineStyle()).HasValue)
                    continue;

                return false;
            }

            _color = color.HasValue ? color.Value : Color.Transparent;
            _width = width.HasValue ? width.Value : Length.Medium;
            _style = style.HasValue ? style.Value : LineStyle.None;
            return true;
        }

        #endregion
    }
}
