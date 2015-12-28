namespace AngleSharp.Dom.Css
{
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS @keyframe rule.
    /// </summary>
    sealed class CssKeyframeRule : CssRule, ICssKeyframeRule
    {
        #region Fields

        readonly CssStyleDeclaration _style;
        IKeyframeSelector _selector;

        #endregion

        #region ctor

        internal CssKeyframeRule(CssParser parser)
            : base(CssRuleType.Keyframe, parser)
        {
            _style = new CssStyleDeclaration(this);
            Children = GetChildren();
        }

        #endregion

        #region Properties

        public String KeyText
        {
            get { return _selector.Text; }
            set
            {
                var selector = Parser.ParseKeyframeSelector(value);

                if (selector == null)
                {
                    throw new DomException(DomError.Syntax);
                }

                _selector = selector;
            }
        }

        public IKeyframeSelector Key
        {
            get { return _selector; }
            set { _selector = value; }
        }

        ICssStyleDeclaration ICssKeyframeRule.Style
        {
            get { return _style; }
        }

        public CssStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (CssKeyframeRule)rule;
            _selector = newRule._selector;
            _style.Clear();
            _style.SetDeclarations(newRule._style.Declarations);
        }

        IEnumerable<ICssNode> GetChildren()
        {
            yield return _selector;
            yield return _style;
        }

        #endregion

        #region String Representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = _style.ToCss(formatter);
            return formatter.Style(KeyText, rules);
        }

        #endregion
    }
}
