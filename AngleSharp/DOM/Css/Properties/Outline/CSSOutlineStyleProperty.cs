namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-style
    /// </summary>
    public sealed class CSSOutlineStyleProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, LineStyle> _styles = new Dictionary<String, LineStyle>(StringComparer.OrdinalIgnoreCase);
        LineStyle _style;

        #endregion

        #region ctor

        static CSSOutlineStyleProperty()
        {
            _styles.Add("none", LineStyle.None);
            _styles.Add("solid", LineStyle.Solid);
            _styles.Add("double", LineStyle.Double);
            _styles.Add("dotted", LineStyle.Dotted);
            _styles.Add("dashed", LineStyle.Dashed);
            _styles.Add("inset", LineStyle.Inset);
            _styles.Add("outset", LineStyle.Outset);
            _styles.Add("ridge", LineStyle.Ridge);
            _styles.Add("groove", LineStyle.Groove);
        }

        internal CSSOutlineStyleProperty()
            : base(PropertyNames.OutlineStyle)
        {
            _inherited = false;
            _style = LineStyle.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected outline style.
        /// </summary>
        public LineStyle Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            LineStyle style;

            if (value is CSSIdentifierValue && _styles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                _style = style;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
