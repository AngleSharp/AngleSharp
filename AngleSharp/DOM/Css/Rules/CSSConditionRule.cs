using System;

namespace AngleSharp.DOM.Css
{
    abstract class CSSConditionRule : CSSGroupingRule
    {
        #region Members

        String conditionText;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the condition of the rule.
        /// </summary>
        public String ConditionText
        {
            get { return conditionText; }
            set { conditionText = value; }
        }

        #endregion
    }
}
