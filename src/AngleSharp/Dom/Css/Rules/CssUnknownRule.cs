namespace AngleSharp.Dom.Css
{
    using AngleSharp.Parser.Css;
    using System;
    using System.IO;

    /// <summary>
    /// Represents an unknown CSS rule.
    /// </summary>
    sealed class CssUnknownRule : CssRule
    {
        #region Fields

        readonly String _name;

        #endregion

        #region ctor

        public CssUnknownRule(String name, CssParser parser)
            : base(CssRuleType.Unknown, parser)
        {
            _name = name;
        }

        #endregion

        #region Properties

        public String Name
        {
            get { return _name; }
        }

        #endregion

        #region Methods

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(SourceCode?.Text);
        }

        #endregion
    }
}
