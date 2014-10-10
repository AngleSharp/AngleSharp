namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline
    /// </summary>
    sealed class CSSOutlineProperty : CSSProperty, ICssOutlineProperty
    {
        #region Fields

        LineStyle _style;
        Length _width;
        IBitmap _color;

        #endregion

        #region ctor

        internal CSSOutlineProperty()
            : base(PropertyNames.Outline, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected outline style property.
        /// </summary>
        public LineStyle Style
        {
            get { return _style; }
        }

        /// <summary>
        /// Gets the selected outline width property.
        /// </summary>
        public Length Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the selected outline color property.
        /// </summary>
        public Color Color
        {
            get { return _color is Color ? (Color)_color : Color.Transparent; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _style = LineStyle.None;
            _width = Length.Medium;
            _color = Colors.Invert;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var entries = value.AsEnumeration();
            LineStyle? style = null;
            Length? width = null;
            IBitmap color = null;

            foreach (var entry in entries)
            {
                if (style == null && (style = entry.ToLineStyle()).HasValue)
                    continue;
                else if (width == null && (width = entry.ToBorderWidth()).HasValue)
                    continue;
                else if (color == null)
                {
                    var c = entry.ToColor();

                    if (c.HasValue)
                    {
                        color = c.Value;
                        continue;
                    }
                    else if (entry.Is(Keywords.Invert))
                    {
                        color = Colors.Invert;
                        continue;
                    }
                }

                return false;
            }

            _style = style ?? LineStyle.None;
            _width = width ?? Length.Medium;
            _color = color ?? Colors.Invert;
            return true;
        }

        #endregion
    }
}
