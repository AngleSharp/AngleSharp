namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Represents an unknown / invalid selector.
    /// </summary>
    sealed class UnknownSelector : ISelector
    {
        readonly String _text;

        public UnknownSelector(String text)
        {
            _text = text;
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
            get { return ToCss(); }
        }

        public String ToCss()
        {
            return _text;
        }

        public String ToCss(IStyleFormatter formatter)
        {
            return ToCss();
        }
    }
}
