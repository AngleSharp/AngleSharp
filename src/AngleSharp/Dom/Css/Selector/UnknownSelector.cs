namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.IO;

    /// <summary>
    /// Represents an unknown / invalid selector.
    /// </summary>
    sealed class UnknownSelector : CssNode, ISelector
    {
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

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(SourceCode?.Text);
        }
    }
}
