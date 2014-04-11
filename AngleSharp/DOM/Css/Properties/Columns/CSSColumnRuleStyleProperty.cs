namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-style
    /// </summary>
    sealed class CSSColumnRuleStyleProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BorderStyle> _styles = new Dictionary<String, BorderStyle>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// The styling must be interpreted as in the collapsing border model.
        /// </summary>
        BorderStyle _style;

        #endregion

        #region ctor

        static CSSColumnRuleStyleProperty()
        {
            _styles.Add("none", BorderStyle.None);
            _styles.Add("hidden", BorderStyle.Hidden);
            _styles.Add("dotted", BorderStyle.Dotted);
            _styles.Add("dashed", BorderStyle.Dashed);
            _styles.Add("solid", BorderStyle.Solid);
            _styles.Add("double", BorderStyle.Double);
            _styles.Add("groove", BorderStyle.Groove);
            _styles.Add("ridge", BorderStyle.Ridge);
            _styles.Add("inset", BorderStyle.Inset);
            _styles.Add("outset", BorderStyle.Outset);
        }

        public CSSColumnRuleStyleProperty()
            : base(PropertyNames.ColumnRuleStyle)
        {
            _style = BorderStyle.None;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            BorderStyle style;

            if (value is CSSIdentifierValue && _styles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                _style = style;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Style Enumeration

        enum BorderStyle
        {
            /// <summary>
            /// No border; the computed border width is zero.
            /// </summary>
            None,
            /// <summary>
            /// Same as 'none', except in terms of border conflict resolution
            /// for table elements.
            /// </summary>
            Hidden,
            /// <summary>
            /// The border is a series of dots.
            /// </summary>
            Dotted,
            /// <summary>
            /// The border is a series of short line segments.
            /// </summary>
            Dashed,
            /// <summary>
            /// The border is a single line segment.
            /// </summary>
            Solid,
            /// <summary>
            /// The border is two solid lines. The sum of the two lines and
            /// the space between them equals the value of 'border-width'.
            /// </summary>
            Double,
            /// <summary>
            /// The border looks as though it were carved into the canvas.
            /// </summary>
            Groove,
            /// <summary>
            /// The opposite of 'groove': the border looks as though it were
            /// coming out of the canvas.
            /// </summary>
            Ridge,
            /// <summary>
            /// The border makes the box look as though it were embedded in
            /// the canvas.
            /// </summary>
            Inset,
            /// <summary>
            /// The opposite of 'inset': the border makes the box look as
            /// though it were coming out of the canvas.
            /// </summary>
            Outset
        }

        #endregion
    }
}
