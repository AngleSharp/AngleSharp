namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents an @namespace rule.
    /// </summary>
    sealed class CssNamespaceRule : CssRule, ICssNamespaceRule
    {
        #region Fields

        String _namespaceURI;
        String _prefix;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @namespace rule.
        /// </summary>
        internal CssNamespaceRule()
            : base(CssRuleType.Namespace)
        {
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
                _namespaceURI = value ?? String.Empty;
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
                _prefix = value ?? String.Empty;
            }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CssNamespaceRule;
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
                        throw new DomException(DomError.InvalidState);
                }
            }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected override String ToCss()
        {
            var prefix = String.IsNullOrEmpty(_prefix) ? String.Empty : _prefix + " ";
            return String.Concat("@namespace ", prefix, _namespaceURI.CssUrl(), ";");
        }

        #endregion
    }
}
