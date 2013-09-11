using System;
using System.Collections.Generic;
using System.Text;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Contains the rules specified by a
    /// @document { /* ... */ } rule.
    /// </summary>
    [DOM("CSSDocumentRule")]
    public sealed class CSSDocumentRule : CSSGroupingRule
    {
        #region Members

        List<Tuple<DocumentFunction, String>> _conditions;

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
        [DOM("conditionText")]
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
                            sb.Append(FunctionNames.URL);
                            break;
                        case DocumentFunction.UrlPrefix:
                            sb.Append(FunctionNames.URL_PREFIX);
                            break;
                        case DocumentFunction.Domain:
                            sb.Append(FunctionNames.DOMAIN);
                            break;
                        case DocumentFunction.RegExp:
                            sb.Append(FunctionNames.REGEXP);
                            break;
                    }

                    sb.Append(Specification.RBO);
                    sb.Append(Specification.DQ);
                    sb.Append(condition.Item2);
                    sb.Append(Specification.DQ);
                    sb.Append(Specification.RBC);
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

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return "@document " + ConditionText + " {" + Environment.NewLine + CssRules.ToCss() + "}";
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
