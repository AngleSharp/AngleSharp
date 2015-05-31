namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    /// <summary>
    /// Contains the rules specified by a
    /// @document { /* ... */ } rule.
    /// </summary>
    sealed class CssDocumentRule : CssGroupingRule, ICssDocumentRule
    {
        #region Fields

        readonly List<IFunction> _conditions;

        #endregion

        #region ctor

        internal CssDocumentRule()
            : base(CssRuleType.Document)
        {
            _conditions = new List<IFunction>();
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
                var conditions = CssParser.ParseDocumentRules(value);

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
        public List<IFunction> Conditions
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

        #region Condition Functions

        public interface IFunction
        {
            String Name { get; }

            String Data { get; }
        }

        /// <summary>
        /// Take as url function.
        /// </summary>
        public class UrlFunction : IFunction
        {
            public UrlFunction(String url)
            {
                Data = url;
            }

            public String Name
            {
                get { return FunctionNames.Url; }
            }

            public String Data
            {
                get;
                private set;
            }
        }

        /// <summary>
        /// Take as a url prefix function.
        /// </summary>
        public class UrlPrefixFunction : IFunction
        {
            public UrlPrefixFunction(String url)
            {
                Data = url;
            }

            public String Name
            {
                get { return FunctionNames.Url_Prefix; }
            }

            public String Data
            {
                get;
                private set;
            }
        }

        /// <summary>
        /// Take as domain.
        /// </summary>
        public class DomainFunction : IFunction
        {
            public DomainFunction(String url)
            {
                Data = url;
            }

            public String Name
            {
                get { return FunctionNames.Domain; }
            }

            public String Data
            {
                get;
                private set;
            }
        }

        /// <summary>
        /// Use regular expression function.
        /// </summary>
        public class RegexpFunction : IFunction
        {
            public RegexpFunction(String url)
            {
                Data = url;
            }

            public String Name
            {
                get { return FunctionNames.Regexp; }
            }

            public String Data
            {
                get;
                private set;
            }
        }

        #endregion
    }
}
