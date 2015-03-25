namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an @supports rule.
    /// </summary>
    sealed class CssSupportsRule : CssConditionRule, ICssSupportsRule
    {
        #region Fields

        ICondition _condition;

        static readonly ICondition empty = new EmptyCondition();

        #endregion

        #region ctor

        internal CssSupportsRule()
            : base(CssRuleType.Supports)
        {
            _condition = empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the condition of the supports rule.
        /// </summary>
        public String ConditionText
        {
            get { return _condition.Text; }
            set
            {
                var condition = CssParser.ParseCondition(value);

                if (condition == null)
                    throw new DomException(DomError.Syntax);

                _condition = condition;
            }
        }

        /// <summary>
        /// Gets or sets the condition of the supports rule.
        /// </summary>
        public ICondition Condition
        {
            get { return _condition; }
            set { _condition = value ?? empty; }
        }

        /// <summary>
        /// Gets if the rule is used, i.e. if the condition is fulfilled.
        /// </summary>
        public Boolean IsSupported
        {
            get { return _condition.Check(); }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = rule as CssSupportsRule;
            ConditionText = newRule.ConditionText;
        }

        internal override Boolean IsValid(RenderDevice device)
        {
            return true;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected override String ToCss()
        {
            return String.Concat("@supports ", ConditionText, " ", Rules.ToCssBlock());
        }

        #endregion

        #region Rules

        public interface ICondition
        {
            Boolean Check();

            String Text { get; }
        }

        public sealed class AndCondition : ICondition
        {
            readonly ICondition[] _conditions;

            public AndCondition(IEnumerable<ICondition> conditions)
            {
                _conditions = conditions.ToArray();
            }

            public String Text
            {
                get { return String.Join(" and ", _conditions.Select(m => m.Text)); }
            }

            public Boolean Check()
            {
                foreach (var condition in _conditions)
                {
                    if (condition.Check() == false)
                        return false;
                }

                return true;
            }
        }

        public sealed class OrCondition : ICondition
        {
            readonly ICondition[] _conditions;

            public OrCondition(IEnumerable<ICondition> conditions)
            {
                _conditions = conditions.ToArray();
            }

            public String Text
            {
                get { return String.Join(" or ", _conditions.Select(m => m.Text)); }
            }

            public Boolean Check()
            {
                foreach (var condition in _conditions)
                {
                    if (condition.Check() == true)
                        return true;
                }

                return false;
            }
        }

        public sealed class NotCondition : ICondition
        {
            readonly ICondition _content;

            public NotCondition(ICondition content)
            {
                _content = content;
            }

            public String Text
            {
                get { return String.Concat("not ", _content.Text); }
            }

            public Boolean Check()
            {
                return !_content.Check();
            }
        }

        sealed class EmptyCondition : ICondition
        {
            public String Text
            {
                get { return String.Empty; }
            }

            public Boolean Check()
            {
                return true;
            }
        }

        public sealed class DeclarationCondition : ICondition
        {
            readonly CssProperty _property;
            readonly ICssValue _value;

            public DeclarationCondition(CssProperty property, ICssValue value)
            {
                _property = property;
                _value = value;
            }

            public String Text
            {
                get
                {
                    var important = _property.IsImportant ? " !important" : String.Empty;
                    var rest = String.Concat(_value.CssText, important, ")");
                    return String.Concat("(", _property.Name, ": ", rest);
                }
            }

            public Boolean Check()
            {
                return (_property is CssUnknownProperty == false) && _property.TrySetValue(_value);
            }
        }

        public sealed class GroupCondition : ICondition
        {
            readonly ICondition _content;

            public GroupCondition(ICondition content)
            {
                _content = content;
            }

            public String Text
            {
                get { return String.Concat("(", _content.Text, ")"); }
            }

            public Boolean Check()
            {
                return _content.Check();
            }
        }

        #endregion
    }
}
