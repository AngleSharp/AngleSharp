namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    sealed class NamespaceSelector : ISelector
    {
        private readonly String _prefix;

        public NamespaceSelector(String prefix)
        {
            _prefix = prefix;
        }

        public Priority Specificity
        {
            get { return Priority.Zero; }
        }

        public String Text
        {
            get { return _prefix; }
        }

        public Boolean Match(IElement element, IElement scope)
        {
            return element.MatchesCssNamespace(_prefix);
        }

        public void Accept(ISelectorVisitor visitor)
        {
            visitor.Type(_prefix);
        }
    }
}
