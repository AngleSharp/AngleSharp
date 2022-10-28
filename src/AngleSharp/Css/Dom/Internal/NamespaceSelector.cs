namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    sealed class NamespaceSelector : ISelector
    {
        private readonly String _prefix;

        /// <summary>
        /// </summary>
        /// <param name="prefix">The escaped prefix text</param>
        public NamespaceSelector(String prefix)
        {
            _prefix = prefix;
        }

        public Priority Specificity => Priority.Zero;

        public String Text => CssUtilities.Escape(_prefix);

        public Boolean Match(IElement element, IElement? scope) => element.MatchesCssNamespace(_prefix);

        public void Accept(ISelectorVisitor visitor) => visitor.Type(_prefix);
    }
}
