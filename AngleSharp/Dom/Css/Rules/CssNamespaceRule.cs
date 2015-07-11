namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents an @namespace rule.
    /// </summary>
    sealed class CssNamespaceRule : CssRule, ICssNamespaceRule
    {
        #region Fields

        String _namespaceUri;
        String _prefix;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @namespace rule.
        /// </summary>
        internal CssNamespaceRule(CssParserOptions options)
            : base(CssRuleType.Namespace, options)
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
            get { return _namespaceUri; }
            set 
            {
                CheckValidity();
                _namespaceUri = value ?? String.Empty;
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
            _namespaceUri = newRule._namespaceUri;
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

        public override String ToCss(IStyleFormatter formatter)
        {
            var space = String.IsNullOrEmpty(_prefix) ? String.Empty : " ";
            var value = String.Concat(_prefix, space, _namespaceUri.CssUrl());
            return formatter.Rule("@namespace", value);
        }

        #endregion
    }
}
