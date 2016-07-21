namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.IO;

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

        internal CssNamespaceRule(CssParser parser)
            : base(CssRuleType.Namespace, parser)
        {
        }

        #endregion

        #region Properties

        public String NamespaceUri
        {
            get { return _namespaceUri; }
            set 
            {
                CheckValidity();
                _namespaceUri = value ?? String.Empty;
            }
        }

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
            base.ReplaceWith(rule);
        }

        #endregion

        #region String Representation

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var space = String.IsNullOrEmpty(_prefix) ? String.Empty : " ";
            var value = String.Concat(_prefix, space, _namespaceUri.CssUrl());
            writer.Write(formatter.Rule("@namespace", value));
        }

        #endregion

        #region Helpers

        static Boolean IsNotSupported(CssRuleType type)
        {
            return type != CssRuleType.Charset && type != CssRuleType.Import && type != CssRuleType.Namespace;
        }

        void CheckValidity()
        {
            var parent = Owner;
            var list = parent != null ? parent.Rules : null;

            if (list != null)
            {
                foreach (var entry in list)
                {
                    if (IsNotSupported(entry.Type))
                        throw new DomException(DomError.InvalidState);
                }
            }
        }

        #endregion
    }
}
