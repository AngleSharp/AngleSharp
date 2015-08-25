namespace AngleSharp.Dom.Css
{
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an unknown CSS rule.
    /// </summary>
    sealed class CssUnknownRule : CssRule
    {
        #region Fields

        readonly String _name;
        readonly List<CssToken> _prelude;
        readonly List<CssToken> _content;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new unknown rule.
        /// </summary>
        public CssUnknownRule(String name, CssParser parser)
            : base(CssRuleType.Unknown, parser)
        {
            _name = name;
            _prelude = new List<CssToken>();
            _content = new List<CssToken>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the @-rule.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the key token list of the unknown rule.
        /// </summary>
        public List<CssToken> Prelude
        {
            get { return _prelude; }
        }

        /// <summary>
        /// Gets the content token list of the unknown rule.
        /// </summary>
        public List<CssToken> Content
        {
            get { return _content; }
        }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            _prelude.Clear();
            _content.Clear();
            var newRule = rule as CssUnknownRule;
            _prelude.AddRange(newRule._prelude);
            _content.AddRange(newRule._content);
        }

        public override String ToCss(IStyleFormatter formatter)
        {
            var prelude = String.Join("", _prelude.Select(m => m.ToValue()));
            var content = String.Join("", _content.Select(m => m.ToValue()));
            var source = String.Concat("@", _name, prelude, content);
            return source;
        }

        #endregion
    }
}
