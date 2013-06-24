using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the abstract base class for
    /// CSS media and CSS supports rules.
    /// </summary>
    public abstract class CSSConditionRule : CSSGroupingRule
    {
        #region ctor

        /// <summary>
        /// Constructs a new CSS condition rule.
        /// </summary>
        internal CSSConditionRule ()
	    { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the condition of the rule.
        /// </summary>
        [DOM("conditionText")]
        public virtual String ConditionText
        {
            get { return String.Empty; }
            set { }
        }

        #endregion
    }
}
