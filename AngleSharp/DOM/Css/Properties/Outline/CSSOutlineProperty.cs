namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline
    /// </summary>
    sealed class CSSOutlineProperty : CSSShorthandProperty, ICssOutlineProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Length, LineStyle, Color?>> Converter = WithAny(
            CSSOutlineWidthProperty.Converter.Option(CSSOutlineWidthProperty.Default),
            CSSOutlineStyleProperty.Converter.Option(CSSOutlineStyleProperty.Default),
            CSSOutlineColorProperty.Converter.Option(CSSOutlineColorProperty.Default));

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

        /// <summary>
        /// Gets if the color should be inverted.
        /// </summary>
        public Boolean IsInverted
        {
            get { return _color.IsInverted; }
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
                    _width.SetWidth(m.Item1);
                    _style.SetStyle(m.Item2);
                    _color.SetColor(m.Item3);
                });
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
