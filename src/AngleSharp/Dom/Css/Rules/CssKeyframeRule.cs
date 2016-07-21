namespace AngleSharp.Dom.Css
{
    using AngleSharp.Parser.Css;
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a CSS @keyframe rule.
    /// </summary>
    sealed class CssKeyframeRule : CssRule, ICssKeyframeRule
    {
        #region ctor

        internal CssKeyframeRule(CssParser parser)
            : base(CssRuleType.Keyframe, parser)
        {
            AppendChild(new CssStyleDeclaration(this));
        }

        #endregion

        #region Properties

        public String KeyText
        {
            get { return Key.Text; }
            set
            {
                var selector = Parser.ParseKeyframeSelector(value);

                if (selector == null)
                    throw new DomException(DomError.Syntax);

                Key = selector;
            }
        }

        public IKeyframeSelector Key
        {
            get { return Children.OfType<IKeyframeSelector>().FirstOrDefault(); }
            set { ReplaceSingle(Key, value); }
        }

        ICssStyleDeclaration ICssKeyframeRule.Style
        {
            get { return Style; }
        }

        public CssStyleDeclaration Style
        {
            get { return Children.OfType<CssStyleDeclaration>().FirstOrDefault(); }
        }

        #endregion

        #region String Representation

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Style(KeyText, Style));
        }

        #endregion
    }
}
