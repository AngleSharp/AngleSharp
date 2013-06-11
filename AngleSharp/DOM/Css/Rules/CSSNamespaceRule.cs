using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an @namespace rule.
    /// </summary>
    sealed class CSSNamespaceRule : CSSRule
    {
        #region Members

        string _namespaceURI;
        string _prefix;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @namespace rule.
        /// </summary>
        public CSSNamespaceRule()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a string containing the text of the URI of the given namespace.
        /// </summary>
        public string NamespaceURI
        {
            get { return _namespaceURI; }
            internal set { _namespaceURI = value; }
        }

        /// <summary>
        /// Gets a string with the name of the prefix associated to this namespace. If there is no such prefix, returns null.
        /// </summary>
        public string Prefix
        {
            get { return _prefix; }
            internal set { _prefix = value; }
        }

        #endregion
    }
}
