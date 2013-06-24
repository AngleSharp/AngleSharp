using AngleSharp.Css;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS rule.
    /// </summary>
    [DOM("CSSRule")]
    public abstract class CSSRule : ICSSObject
    {
        #region Members

        /// <summary>
        /// The type of CSS rule.
        /// </summary>
        protected CssRule _type;
        /// <summary>
        /// The parent stylesheet.
        /// </summary>
        protected CSSStyleSheet _parent;
        /// <summary>
        /// The parent rule.
        /// </summary>
        protected CSSRule _parentRule;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS rule.
        /// </summary>
        internal CSSRule()
        {
            _type = CssRule.Unknown;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the textual representation of the rule.
        /// </summary>
        [DOM("cssText")]
        public String CssText
        {
            get { return ToCss(); }
        }

        /// <summary>
        /// Gets the containing rule, otherwise null.
        /// </summary>
        [DOM("parentRule")]
        public CSSRule ParentRule
        {
            get { return _parentRule; }
            internal set { _parentRule = value; }
        }

        /// <summary>
        /// Gets the CSSStyleSheet object for the style sheet that contains this rule.
        /// </summary>
        [DOM("parentStyleSheet")]
        public CSSStyleSheet ParentStyleSheet
        {
            get { return _parent; }
            internal set { _parent = value; }
        }


        /// <summary>
        /// Gets the type constant indicating the type of CSS rule.
        /// </summary>
        [DOM("type")]
        public CssRule Type
        {
            get { return _type; }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public abstract String ToCss();

        #endregion
    }
}
