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

        readonly List<Tuple<DocumentFunction, String>> _conditions;

        #endregion

        #region ctor

        internal CssDocumentRule()
            : base(CssRuleType.Document)
        {
            _conditions = new List<Tuple<DocumentFunction, String>>();
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
                    var name = GetFunctionName(condition.Item1);
                    var value = condition.Item2.CssString();
                    entries[i] = String.Concat(name, "(", value, ")");
			    }

                return String.Join(", ", entries); 
            }
            set
            {
                var conditions = CssParser.ParseDocumentRules(value);

                if (conditions == null)
                    return;

                _conditions.Clear();
                _conditions.AddRange(conditions);
            }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the list with the conditions.
        /// </summary>
        internal List<Tuple<DocumentFunction, String>> Conditions
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

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected override String ToCss()
        {
            return String.Concat("@document ", ConditionText, " ", Rules.ToCssBlock());
        }

        #endregion

        #region Enum

        static String GetFunctionName(DocumentFunction function)
        {
            switch (function)
            {
                case DocumentFunction.Url:
                    return FunctionNames.Url;
                case DocumentFunction.UrlPrefix:
                    return FunctionNames.Url_Prefix;
                case DocumentFunction.Domain:
                    return FunctionNames.Domain;
                case DocumentFunction.RegExp:
                    return FunctionNames.Regexp;
            }

            return String.Empty;
        }

        /// <summary>
        /// An enumeration over possible functions.
        /// </summary>
        public enum DocumentFunction
        {
            /// <summary>
            /// Take as url function.
            /// </summary>
            Url,
            /// <summary>
            /// Take as a url prefix function.
            /// </summary>
            UrlPrefix,
            /// <summary>
            /// Take as domain.
            /// </summary>
            Domain,
            /// <summary>
            /// Use regular expression function.
            /// </summary>
            RegExp
        }

        #endregion
    }
}
