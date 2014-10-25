namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration
    /// </summary>
    sealed class CSSTextDecorationProperty : CSSShorthandProperty, ICssTextDecorationProperty
    {
        #region Fields

        readonly CSSTextDecorationColorProperty _color;
        readonly CSSTextDecorationLineProperty _line;
        readonly CSSTextDecorationStyleProperty _style;

        #endregion

        #region ctor

        internal CSSTextDecorationProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.TextDecoration, rule, PropertyFlags.Animatable)
        {
            _color = Get<CSSTextDecorationColorProperty>();
            _line = Get<CSSTextDecorationLineProperty>();
            _style = Get<CSSTextDecorationStyleProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the decoration style property.
        /// </summary>
        public TextDecorationStyle DecorationStyle
        {
            get { return _style.DecorationStyle; }
        }

        /// <summary>
        /// Gets the value of the line property.
        /// </summary>
        public IEnumerable<TextDecorationLine> Line
        {
            get { return _line.Line; }
        }

        /// <summary>
        /// Gets the value of the color property.
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
            var line = new CSSValueList();
            CSSValue color = null;
            CSSValue style = null;

            if (list.Length > 3)
                return false;

            for (int i = 0; i < list.Length; i++)
            {
                if (_line.CanTake(list[i]))
                    line.Add(list[i]);
                else if (!_color.CanStore(list[i], ref color) && !_style.CanStore(list[i], ref style))
                    return false;
            }

            return _line.TrySetValue(line.Reduce()) && _color.TrySetValue(color) && _style.TrySetValue(style);
        }

        #endregion
    }
}
