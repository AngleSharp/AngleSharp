using AngleSharp.Css;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS rule.
    /// </summary>
    public class CSSRule
    {
        #region Members

        protected CssRule _type;
        protected CSSStyleSheet _parent;
        protected CSSRule _parentRule;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS rule.
        /// </summary>
        public CSSRule()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the textual representation of the rule.
        /// </summary>
        public string CssText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the containing rule, otherwise null.
        /// </summary>
        public CSSRule ParentRule
        {
            get { return _parentRule; }
        }

        /// <summary>
        /// Gets the CSSStyleSheet object for the style sheet that contains this rule.
        /// </summary>
        public CSSStyleSheet ParentStyleSheet
        {
            get { return _parent; }
        }


        /// <summary>
        /// Gets the type constant indicating the type of CSS rule.
        /// </summary>
        public CssRule Type
        {
            get { return _type; }
        }

        #endregion
    }
}
