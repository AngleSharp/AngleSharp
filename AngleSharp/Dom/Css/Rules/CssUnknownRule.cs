namespace AngleSharp.Dom.Css
{
    using AngleSharp.Parser.Css;
    using System;

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

        public override String ToCss(IStyleFormatter formatter)
        {
            var view = SourceCode;
            var text = view != null ? view.Text : String.Empty;
            return text;
        }

        #endregion
    }
}
