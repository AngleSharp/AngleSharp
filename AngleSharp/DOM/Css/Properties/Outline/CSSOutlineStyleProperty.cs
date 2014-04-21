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

        internal CSSOutlineStyleProperty()
            : base(PropertyNames.OutlineStyle)
        {
            _inherited = false;
            _style = OutlineStyle.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected outline style.
        /// </summary>
        public OutlineStyle Style
        {
            get { return _style; }
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
    }
}
