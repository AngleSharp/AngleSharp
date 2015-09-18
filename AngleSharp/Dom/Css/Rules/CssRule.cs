namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS rule.
    /// </summary>
    abstract class CssRule : ICssRule
    {
        #region Fields

        readonly CssRuleType _type;
        readonly CssParser _parser;

        ICssStyleSheet _ownerSheet;
        ICssRule _parentRule;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS rule.
        /// </summary>
        internal CssRule(CssRuleType type, CssParser parser)
        {
            _type = type;
            _parser = parser;
        }

        #endregion

        #region Properties

        public IEnumerable<ICssNode> Children
        {
            get { return Enumerable.Empty<ICssNode>(); }
        }

        public String CssText
        {
            get { return ToCss(); }
            set
            {
                var rule = _parser.ParseRule(value);

                if (rule == null)
                    throw new DomException(DomError.Syntax);
                else if (rule.Type != _type)
                    throw new DomException(DomError.InvalidModification);

                ReplaceWith(rule);
            }
        }

        public ICssRule Parent
        {
            get { return _parentRule; }
            internal set { _parentRule = value; }
        }

        public ICssStyleSheet Owner
        {
            get { return _ownerSheet; }
            internal set { _ownerSheet = value; }
        }

        public CssRuleType Type
        {
            get { return _type; }
        }

        #endregion

        #region Internal Properties

        internal CssParser Parser
        {
            get { return _parser; }
        }

        #endregion

        #region Methods

        public String ToCss()
        {
            return ToCss(CssStyleFormatter.Instance);
        }

        public abstract String ToCss(IStyleFormatter formatter);

        #endregion

        #region Internal Methods

        /// <summary>
        /// Replaces the current object with the given rule.
        /// The types are equal.
        /// </summary>
        /// <param name="rule">The new rule.</param>
        protected abstract void ReplaceWith(ICssRule rule);

        #endregion
    }
}
