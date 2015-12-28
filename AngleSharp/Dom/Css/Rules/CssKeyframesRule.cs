namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents an @keyframes rule.
    /// </summary>
    sealed class CssKeyframesRule : CssRule, ICssKeyframesRule
    {
        #region Fields

        readonly CssRuleList _rules;
        String _name;

        #endregion

        #region ctor

        internal CssKeyframesRule(CssParser parser)
            : base(CssRuleType.Keyframes, parser)
        {
            _rules = new CssRuleList();
            Children = _rules;
        }

        #endregion

        #region Properties

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public CssRuleList Rules
        {
            get { return _rules; }
        }

        ICssRuleList ICssKeyframesRule.Rules
        {
            get { return _rules; }
        }

        #endregion

        #region Methods

        public void Add(String rule)
        {
            var value = Parser.ParseKeyframeRule(rule);
            _rules.Insert(value, _rules.Length, Owner, this);
        }

        public void Remove(String key)
        {
            var element = Find(key);

            if (element != null)
            {
                for (var i = 0; i < _rules.Length; i++)
                {
                    if (element == _rules[i])
                    {
                        _rules.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public ICssKeyframeRule Find(String key)
        {
            return _rules.OfType<ICssKeyframeRule>().FirstOrDefault(m => key.Isi(m.KeyText));
        }

        #endregion

        #region Internal Methods

        internal void AddRule(CssKeyframeRule rule)
        {
            _rules.Add(rule, Owner, this);
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CssKeyframesRule;
            _name = newRule._name;
            _rules.Clear();
            _rules.Import(newRule._rules, Owner, Parent);
        }

        #endregion

        #region String Representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            return formatter.Rule("@keyframes", _name, rules);
        }

        #endregion
    }
}
