using System;
using AngleSharp.DOM.Collections;
using AngleSharp.Css;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS style rule.
    /// </summary>
    [DOM("CSSStyleRule")]
    public sealed class CSSStyleRule : CSSRule
    {
        #region Members

        String _selectorText;
        Selector _selector;
        CSSStyleDeclaration _style;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS style rule.
        /// </summary>
        internal CSSStyleRule()
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
        [DOM("selectorText")]
        public String SelectorText
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
        [DOM("style")]
        public CSSStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return _selectorText + " {" + Environment.NewLine + _style.ToCss() + "}";
        }

        #endregion
    }
}
