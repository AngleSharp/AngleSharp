using System;

namespace AngleSharp.DOM.Css.Rules
{
    /// <summary>
    /// Contains the rules specified by a
    /// @document { /* ... */ } rule.
    /// </summary>
    [DOM("CSSDocumentRule")]
    public sealed class CSSDocumentRule : CSSGroupingRule
    {
        #region Members

        String _url;
        DocumentFunction _function;

        #endregion

        #region ctor

        internal CSSDocumentRule()
        {
            _type = CssRuleType.Document;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the URL to consider.
        /// </summary>
        [DOM("url")]
        public String Url
        {
            get { return _url; }
            internal set { _url = value; }
        }

        /// <summary>
        /// Gets the function to use.
        /// </summary>
        [DOM("function")]
        public DocumentFunction Function
        {
            get { return _function; }
            internal set { _function = value; }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return "@document " + _url + " {" + Environment.NewLine + CssRules.ToCss() + "}";
        }

        #endregion

        #region Enum

        public enum DocumentFunction
        {
            /// <summary>
            /// Take as url function.
            /// </summary>
            Url,
            /// <summary>
            /// Take as a url prefix function.
            /// </summary>
            UrlPrefix,
            /// <summary>
            /// Take as domain.
            /// </summary>
            Domain,
            /// <summary>
            /// Use regular expression function.
            /// </summary>
            RegExp
        }

        #endregion
    }
}
