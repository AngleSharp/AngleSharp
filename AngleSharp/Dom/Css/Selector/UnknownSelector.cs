namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Represents an unknown / invalid selector.
    /// </summary>
    sealed class UnknownSelector : CssNode, ISelector
    {
        public UnknownSelector(TextView source)
        {
            SourceCode = source;
        }

        public Priority Specifity
        {
            get { return Priority.Zero; }
        }

        public Boolean Match(IElement element)
        {
            return false;
        }

        public String Text
        {
            get { return this.ToCss(); }
        }

        public override String ToCss(IStyleFormatter formatter)
        {
            return SourceCode.Text;
        }
    }
}
