namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents an @namespace rule.
    /// </summary>
    [DomName("CSSNamespaceRule")]
    public sealed class CSSNamespaceRule : CSSRule
    {
        #region Fields

        String _namespaceURI;
        String _prefix;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @namespace rule.
        /// </summary>
        internal CSSNamespaceRule()
        {
            _type = CssRuleType.Namespace;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a string containing the text of the URI of the given namespace.
        /// </summary>
        [DomName("namespaceURI")]
        public String NamespaceURI
        {
            get { return _namespaceURI; }
            internal set { _namespaceURI = value; }
        }

        /// <summary>
        /// Gets a string with the name of the prefix associated to this namespace. If there is no such prefix, returns null.
        /// </summary>
        [DomName("prefix")]
        public String Prefix
        {
            get { return _prefix; }
            internal set { _prefix = value; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSNamespaceRule;
            _namespaceURI = newRule._namespaceURI;
            _prefix = newRule._prefix;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Format("@namespace {0} '{1}';", _prefix, _namespaceURI);
        }

        #endregion
    }
}
