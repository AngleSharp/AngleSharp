namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents an @namespace rule.
    /// </summary>
    sealed class CSSNamespaceRule : CSSRule, ICssNamespaceRule
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
        /// Gets or sets a string containing the text of the
        /// URI of the given namespace.
        /// </summary>
        public String NamespaceUri
        {
            get { return _namespaceURI; }
            set 
            {
                CheckValidity();
                _namespaceURI = value; 
            }
        }

        /// <summary>
        /// Gets or sets a string with the name of the prefix
        /// associated to this namespace. If there is no such
        /// prefix, returns null.
        /// </summary>
        public String Prefix
        {
            get { return _prefix; }
            set 
            {
                CheckValidity();
                _prefix = value;
            }
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

        #region Helpers

        void CheckValidity()
        {
            var parent = Owner;
            var list = parent != null ? parent.Rules : null;

            if (list != null)
            {
                foreach (var entry in list)
                {
                    if (entry.Type != CssRuleType.Charset && entry.Type != CssRuleType.Import && entry.Type != CssRuleType.Namespace)
                        throw new DomException(ErrorCode.InvalidState);
                }
            }
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
