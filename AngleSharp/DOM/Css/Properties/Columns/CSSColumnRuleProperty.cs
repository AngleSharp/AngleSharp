namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule
    /// </summary>
    sealed class CSSColumnRuleProperty : CSSShorthandProperty, ICssColumnRuleProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Color, Length, LineStyle>> Converter = WithOptions(
                CSSColumnRuleColorProperty.Converter,
                CSSColumnRuleWidthProperty.Converter,
                CSSColumnRuleStyleProperty.Converter,
            Tuple.Create(CSSColumnRuleColorProperty.Default, CSSColumnRuleWidthProperty.Default, CSSColumnRuleStyleProperty.Default));

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
            return Converter.TryConvert(value, m =>
            {
                _color.SetColor(m.Item1);
                _width.SetWidth(m.Item2);
                _style.SetStyle(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return String.Format("{0} {1} {2}", _width.SerializeValue(), _style.SerializeValue(), _color.SerializeValue());
        }

        #endregion
    }
}
