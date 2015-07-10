namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains the rules specified by a
    /// @document { /* ... */ } rule.
    /// </summary>
    sealed class CssDocumentRule : CssGroupingRule, ICssDocumentRule
    {
        #region Fields

        readonly List<IDocumentFunction> _conditions;
        readonly CssParserOptions _options;

        #endregion

        #region ctor

        internal CssDocumentRule(CssParserOptions options)
            : base(CssRuleType.Document)
        {
            _conditions = new List<IDocumentFunction>();
            _options = options;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the condition text.
        /// </summary>
        public String ConditionText
        {
            get 
            {
                var entries = new String[_conditions.Count];

                for (int i = 0; i < entries.Length; i++)
			    {
                    var condition = _conditions[i];
                    var name = condition.Name;
                    var value = condition.Data.CssString();
                    entries[i] = String.Concat(name, "(", value, ")");
			    }

                return String.Join(", ", entries); 
            }
            set
            {
                var conditions = CssParser.ParseDocumentRules(value, _options);

                if (conditions == null)
                    throw new DomException(DomError.Syntax);

                _conditions.Clear();
                _conditions.AddRange(conditions);
            }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the list with the conditions.
        /// </summary>
        public List<IDocumentFunction> Conditions
        {
            get { return _conditions; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = rule as CssDocumentRule;
            _conditions.Clear();
            _conditions.AddRange(newRule._conditions);
        }

        #endregion

        #region String representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            return formatter.Rule("@document", ConditionText, rules);
        }

        #endregion
    }
}
