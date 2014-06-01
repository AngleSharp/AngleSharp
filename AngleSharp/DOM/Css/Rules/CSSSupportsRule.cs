namespace AngleSharp.DOM.Css
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    [DOM("CSSSupportsRule")]
    public sealed class CSSSupportsRule : CSSConditionRule
    {
        #region Fields

        String _condition;
        Boolean _used;

        #endregion

        #region ctor

        internal CSSSupportsRule()
        {
            _type = CssRuleType.Supports;
            _condition = String.Empty;
            _used = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the condition of the support rule.
        /// </summary>
        [DOM("conditionText")]
        public override String ConditionText
        {
            get { return _condition; }
            set { _condition = value; }
        }

        /// <summary>
        /// Gets if the rule is used.
        /// </summary>
        [DOM("isSupported")]
        public Boolean IsSupported
        {
            get { return _used; }
            internal set { _used = value; }
        }

        #endregion

        #region Internal Methods

        internal override Boolean IsValid(IWindow window)
        {
            //TODO
            return true;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Format("@supports {0} {{{1}{2}}}", _condition, Environment.NewLine, CssRules.ToCss());
        }

        #endregion
    }
}
