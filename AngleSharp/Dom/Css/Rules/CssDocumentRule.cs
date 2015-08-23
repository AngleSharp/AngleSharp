namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
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

        readonly List<CssDocumentFunction> _conditions;

        #endregion

        #region ctor

        internal CssDocumentRule(CssParser parser)
            : base(CssRuleType.Document, parser)
        {
            _conditions = new List<CssDocumentFunction>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the condition text.
        /// </summary>
        public String ConditionText
        {
            get { return Serialize(", "); }
            set
            {
                var conditions = Parser.ParseDocumentRules(value);

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
        public List<CssDocumentFunction> Conditions
        {
            get { return _conditions; }
        }

        #endregion

        #region Methods

        public override String GetSource()
        {
            var rules = base.GetSource();
            var source = String.Concat("@document", Serialize(","), rules);
            return Decorate(source);
        }

        public override IEnumerable<CssNode> GetChildren()
        {
            foreach (var condition in _conditions)
                yield return condition;

            foreach (var child in base.GetChildren())
                yield return child;
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

        String Serialize(String separator)
        {
            var entries = new String[_conditions.Count];

            for (int i = 0; i < entries.Length; i++)
                entries[i] = _conditions[i].GetSource();

            return String.Join(separator, entries);
        }

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            return formatter.Rule("@document", ConditionText, rules);
        }

        #endregion
    }
}
