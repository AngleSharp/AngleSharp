namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.IO;
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
            _rules = new CssRuleList(this);
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

        public void Add(String ruleText)
        {
            var rule = Parser.ParseKeyframeRule(ruleText);
            _rules.Add(rule);
        }

        public void Remove(String key)
        {
            var element = Find(key);
            _rules.Remove(element);
        }

        public CssKeyframeRule Find(String key)
        {
            return _rules.OfType<CssKeyframeRule>().FirstOrDefault(m => key.Isi(m.KeyText));
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            writer.Write(formatter.Rule("@keyframes", _name, rules));
        }

        ICssKeyframeRule ICssKeyframesRule.Find(String key)
        {
            return Find(key);
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CssKeyframesRule;
            _name = newRule._name;
            base.ReplaceWith(rule);
        }

        #endregion
    }
}
