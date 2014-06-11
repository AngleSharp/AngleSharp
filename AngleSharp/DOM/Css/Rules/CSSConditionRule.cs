namespace AngleSharp.DOM.Css
{
    using AngleSharp.DOM.Collections;
    using System;

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
        [DomName("conditionText")]
        public virtual String ConditionText
        {
            get { return String.Empty; }
            set { }
        }

        #endregion

        #region Internal Methods

        internal override void ComputeStyle(CSSStyleDeclaration style, IWindow window, IElement element)
        {
            if (IsValid(window))
                base.ComputeStyle(style, window, element);
        }

        internal abstract Boolean IsValid(IWindow window);

        #endregion
    }
}
