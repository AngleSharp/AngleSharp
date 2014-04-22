namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-style
    /// </summary>
    public sealed class CSSColumnRuleStyleProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, LineStyle> _styles = new Dictionary<String, LineStyle>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// The styling must be interpreted as in the collapsing border model.
        /// </summary>
        LineStyle _style;

        #endregion

        #region ctor

        static CSSColumnRuleStyleProperty()
        {
            _styles.Add("none", LineStyle.None);
            _styles.Add("hidden", LineStyle.Hidden);
            _styles.Add("dotted", LineStyle.Dotted);
            _styles.Add("dashed", LineStyle.Dashed);
            _styles.Add("solid", LineStyle.Solid);
            _styles.Add("double", LineStyle.Double);
            _styles.Add("groove", LineStyle.Groove);
            _styles.Add("ridge", LineStyle.Ridge);
            _styles.Add("inset", LineStyle.Inset);
            _styles.Add("outset", LineStyle.Outset);
        }

        internal CSSColumnRuleStyleProperty()
            : base(PropertyNames.ColumnRuleStyle)
        {
            _style = LineStyle.None;
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected column-rule line style.
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
