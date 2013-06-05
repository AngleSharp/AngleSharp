using System;
using AngleSharp.DOM.Collections;
using AngleSharp.Css;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS style rule.
    /// </summary>
    public sealed class CSSStyleRule : CSSRule
    {
        #region Members

        string _selectorText;
        Selector _selector;
        CSSStyleDeclaration _style;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS style rule.
        /// </summary>
        public CSSStyleRule()
        {
            _type = CssRule.Style;
            _style = new CSSStyleDeclaration();
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets or sets the selector.
        /// </summary>
        internal Selector Selector
        {
            get { return _selector; }
            set
            {
                _selector = value;
                _selectorText = value.ToCss();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the textual representation of the selector for this rule, e.g. "h1,h2".
        /// </summary>
        public string SelectorText
        {
            get { return _selectorText; }
            set
            {
                _selector = CssParser.ParseSelector(value);
                _selectorText = value;
            }
        }

        /// <summary>
        /// Gets the CSSStyleDeclaration object for the rule.
        /// </summary>
        public CSSStyleDeclaration Style
        {
            get { return _style; }
            internal set { _style = value; }
        }

        #endregion

        //Sooner or later required:
        //TODO --- how to connect to the RuleList ?? which is connected to the stylesheet etc.
        //http://www.w3.org/TR/DOM-Level-2-Style/css.html#CSS-CSSStyleDeclaration
    }
}
