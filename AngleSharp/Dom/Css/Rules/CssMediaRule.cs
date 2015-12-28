namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS @media rule.
    /// </summary>
    sealed class CssMediaRule : CssConditionRule, ICssMediaRule
    {
        #region Fields

        readonly MediaList _media;

        #endregion

        #region ctor

        internal CssMediaRule(CssParser parser)
            : base(CssRuleType.Media, parser)
        {
            _media = new MediaList(parser);
            Children = GetChildren();
        }

        #endregion

        #region Properties

        public String ConditionText
        {
            get { return _media.MediaText; }
            set { _media.MediaText = value; }
        }

        public MediaList Media
        {
            get { return _media; }
        }

        IMediaList ICssMediaRule.Media
        {
            get { return _media; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = rule as CssMediaRule;
            _media.Import(newRule._media);
        }

        internal override Boolean IsValid(RenderDevice device)
        {
            return _media.Validate(device);
        }

        IEnumerable<ICssNode> GetChildren()
        {
            yield return _media;

            foreach (var rule in Rules)
            {
                yield return rule;
            }
        }

        #endregion

        #region String Representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            return formatter.Rule("@media", _media.MediaText, rules);
        }

        #endregion
    }
}
