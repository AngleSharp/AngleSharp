namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-style
    /// </summary>
    sealed class CSSOutlineStyleProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, OutlineStyle> _styles = new Dictionary<String, OutlineStyle>(StringComparer.OrdinalIgnoreCase);
        OutlineStyle _style;

        #endregion

        #region ctor

        static CSSOutlineStyleProperty()
        {
            _styles.Add("none", OutlineStyle.None);
            _styles.Add("solid", OutlineStyle.Solid);
            _styles.Add("double", OutlineStyle.Double);
            _styles.Add("dotted", OutlineStyle.Dotted);
            _styles.Add("dashed", OutlineStyle.Dashed);
            _styles.Add("inset", OutlineStyle.Inset);
            _styles.Add("outset", OutlineStyle.Outset);
            _styles.Add("ridge", OutlineStyle.Ridge);
            _styles.Add("groove", OutlineStyle.Groove);
        }

        public CSSOutlineStyleProperty()
            : base(PropertyNames.OutlineStyle)
        {
            _inherited = false;
            _style = OutlineStyle.None;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            OutlineStyle style;

            if (value is CSSIdentifierValue && _styles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                _style = style;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Style Enumeration

        enum OutlineStyle
        {
            /// <summary>
            /// No outline (outline-width is 0).
            /// </summary>
            None,
            /// <summary>
            /// The outline is a series of dots.
            /// </summary>
            Dotted,
            /// <summary>
            /// The outline is a series of short line segments.
            /// </summary>
            Dashed,
            /// <summary>
            /// The outline is a single line.
            /// </summary>
            Solid,
            /// <summary>
            /// The outline is two single lines. The outline-width is the sum of the two lines and the space between them.
            /// </summary>
            Double,
            /// <summary>
            /// The outline looks as though it were carved into the canvas.
            /// </summary>
            Groove,
            /// <summary>
            /// The opposite of groove: the outline looks as though it were coming out of the canvas.
            /// </summary>
            Ridge,
            /// <summary>
            /// The outline makes the box look as though it were embeded in the canvas.
            /// </summary>
            Inset,
            /// <summary>
            /// The opposite of inset: the outline makes the box look as though it were coming out of the canvas.
            /// </summary>
            Outset
        }

        #endregion
    }
}
