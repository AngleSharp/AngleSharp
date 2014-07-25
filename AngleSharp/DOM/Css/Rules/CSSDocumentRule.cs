namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains the rules specified by a
    /// @document { /* ... */ } rule.
    /// </summary>
    [DomName("CSSDocumentRule")]
    public sealed class CSSDocumentRule : CSSGroupingRule
    {
        #region Fields

        readonly List<Tuple<DocumentFunction, String>> _conditions;

        #endregion

        #region ctor

        internal CSSDocumentRule()
        {
            _type = CssRuleType.Document;
            _conditions = new List<Tuple<DocumentFunction, String>>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the condition text.
        /// </summary>
        [DomName("conditionText")]
        public String ConditionText
        {
            get 
            {
                var sb = Pool.NewStringBuilder();
                var co = false;

                foreach (var condition in _conditions)
                {
                    if (co)
                        sb.Append(',');

                    switch (condition.Item1)
                    {
                        case DocumentFunction.Url:
                            sb.Append(FunctionNames.Url);
                            break;
                        case DocumentFunction.UrlPrefix:
                            sb.Append(FunctionNames.Url_Prefix);
                            break;
                        case DocumentFunction.Domain:
                            sb.Append(FunctionNames.Domain);
                            break;
                        case DocumentFunction.RegExp:
                            sb.Append(FunctionNames.Regexp);
                            break;
                    }

                    sb.Append(Specification.RoundBracketOpen);
                    sb.Append(Specification.DoubleQuote);
                    sb.Append(condition.Item2);
                    sb.Append(Specification.DoubleQuote);
                    sb.Append(Specification.RoundBracketClose);
                    co = true;
                }

                return sb.ToPool(); 
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
            var newRule = rule as CSSDocumentRule;
            _conditions.Clear();
            _conditions.AddRange(newRule._conditions);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return "@document " + ConditionText + " {" + Environment.NewLine + Rules.ToCss() + "}";
        }

        #endregion

        #region Enum

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
