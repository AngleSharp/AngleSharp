namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-style
    /// </summary>
    sealed class CSSTextDecorationStyleProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, TextDecorationStyle> _styles = new Dictionary<String, TextDecorationStyle>(StringComparer.OrdinalIgnoreCase);
        TextDecorationStyle _style;

        #endregion

        #region ctor

        static CSSTextDecorationStyleProperty()
        {
            _styles.Add("solid", TextDecorationStyle.Solid);
            _styles.Add("double", TextDecorationStyle.Double);
            _styles.Add("dotted", TextDecorationStyle.Dotted);
            _styles.Add("dashed", TextDecorationStyle.Dashed);
            _styles.Add("wavy", TextDecorationStyle.Wavy);
        }

        public CSSTextDecorationStyleProperty()
            : base(PropertyNames.TextDecorationStyle)
        {
            _inherited = false;
            _style = TextDecorationStyle.Solid;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            TextDecorationStyle style;

            if (value is CSSIdentifierValue && _styles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                _style = style;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Style Enumeration

        enum TextDecorationStyle
        {
            /// <summary>
            /// Draws a single line.
            /// </summary>
            Solid,
            /// <summary>
            /// Draws a double line.
            /// </summary>
            Double,
            /// <summary>
            /// Draws a dotted line.
            /// </summary>
            Dotted,
            /// <summary>
            /// Draws a dashed line.
            /// </summary>
            Dashed,
            /// <summary>
            /// Draws a wavy line.
            /// </summary>
            Wavy
        }

        #endregion
    }
}
