using System;

namespace AngleSharp.DOM.Css
{
    abstract class CSSConditionRule : CSSGroupingRule
    {
        #region Members

        string conditionText;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the condition of the rule.
        /// </summary>
        public string ConditionText
        {
            get { return conditionText; }
            set { conditionText = value; }
        }

        #endregion
    }
}
