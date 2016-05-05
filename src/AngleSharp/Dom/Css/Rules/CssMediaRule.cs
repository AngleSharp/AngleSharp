namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Parser.Css;
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a CSS @media rule.
    /// </summary>
    sealed class CssMediaRule : CssConditionRule, ICssMediaRule
    {
        #region ctor

        internal CssMediaRule(CssParser parser)
            : base(CssRuleType.Media, parser)
        {
            AppendChild(new MediaList(parser));
        }

        #endregion

        #region Properties

        public String ConditionText
        {
            get { return Media.MediaText; }
            set { Media.MediaText = value; }
        }

        public MediaList Media
        {
            get { return Children.OfType<MediaList>().FirstOrDefault(); }
        }

        IMediaList ICssMediaRule.Media
        {
            get { return Media; }
        }

        #endregion

        #region Internal Methods

        internal override Boolean IsValid(RenderDevice device)
        {
            return Media.Validate(device);
        }

        #endregion

        #region Methods

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = formatter.Block(Rules);
            writer.Write(formatter.Rule("@media", Media.MediaText, rules));
        }

        #endregion
    }
}
