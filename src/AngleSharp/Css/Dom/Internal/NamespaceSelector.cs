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

        public Priority Specificity => Priority.Zero;

        public String Text => _prefix;

        public Boolean Match(IElement element, IElement scope) => element.MatchesCssNamespace(_prefix);

        public void Accept(ISelectorVisitor visitor) => visitor.Type(_prefix);
    }
}
