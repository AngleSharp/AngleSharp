using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an @namespace rule.
    /// </summary>
    public sealed class CSSNamespaceRule : CSSRule
    {
        #region Constants

        internal const String RuleName = "namespace";

        #endregion

        #region Members

        String _namespaceURI;
        String _prefix;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @namespace rule.
        /// </summary>
        internal CSSNamespaceRule()
        {
            _type = CssRule.Namespace;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a string containing the text of the URI of the given namespace.
        /// </summary>
        public String NamespaceURI
        {
            get { return _namespaceURI; }
            internal set { _namespaceURI = value; }
        }

        /// <summary>
        /// Gets a string with the name of the prefix associated to this namespace. If there is no such prefix, returns null.
        /// </summary>
        public String Prefix
        {
            get { return _prefix; }
            internal set { _prefix = value; }
        }

        #endregion
    }
}
