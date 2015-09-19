namespace AngleSharp.Dom.Css
{
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

        #endregion

        #region ctor

        internal CssDocumentRule(CssParser parser)
            : base(CssRuleType.Document, parser)
        {
            _conditions = new List<IDocumentFunction>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the condition text.
        /// </summary>
        public String ConditionText
        {
            get
            {
                var entries = new String[_conditions.Count];

                for (int i = 0; i < entries.Length; i++)
                    entries[i] = _conditions[i].ToCss();

                return String.Join(", ", entries); 
            }
            set
            {
                var conditions = Parser.ParseDocumentRules(value);

                if (conditions == null)
                    throw new DomException(DomError.Syntax);

                _conditions.Clear();
                _conditions.AddRange(conditions);
            }
        }

        /// <summary>
        /// Gets an enumerable with the conditions.
        /// </summary>
        public IEnumerable<IDocumentFunction> Conditions
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

        /// <summary>
        /// Adds a condition to the list of conditions.
        /// </summary>
        /// <param name="condition">The condition to add.</param>
        internal void AddCondition(IDocumentFunction condition)
        {
            _conditions.Add(condition);
        }

        /// <summary>
        /// Removes a condition from the list of conditions.
        /// </summary>
        /// <param name="condition">The condition to remove.</param>
        internal void RemoveCondition(IDocumentFunction condition)
        {
            _conditions.Remove(condition);
        }

        /// <summary>
        /// Checks if the rule should be active for the provided URL.
        /// </summary>
        /// <param name="url">The URL to examine.</param>
        /// <returns>True if the URL matches one of the conditions.</returns>
        internal Boolean IsValid(Url url)
        {
            foreach (var condition in _conditions)
            {
                if (condition.Matches(url))
                    return true;
            }

            return false;
        }

        #endregion

        #region String Representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            return formatter.Rule("@document", ConditionText, rules);
        }

        #endregion
    }
}
