namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    internal sealed class LanguageSelector : ISelector
    {
        private readonly String _language;

        public LanguageSelector(String language)
        {
            _language = language;
        }

        public Priority Specificity => Priority.OneClass;

        public String Text => PseudoClassNames.Separator + PseudoClassNames.Lang.CssFunction(_language);

        public void Accept(ISelectorVisitor visitor) => visitor.PseudoClass(PseudoClassNames.Lang.CssFunction(_language));

        public Boolean Match(IElement element, IElement? scope)
        {
            var language = GetLanguage(element);
            return language is not null && (
                language.Equals(_language, StringComparison.OrdinalIgnoreCase) ||
                language.StartsWith(_language + "-", StringComparison.OrdinalIgnoreCase));
        }

        private static String? GetLanguage(IElement element)
        {
            for (var current = element; current is not null; current = current.ParentElement)
            {
                var language = current.GetAttribute(NamespaceNames.XmlUri, AttributeNames.Lang);

                if (language is null && (current.NamespaceUri == NamespaceNames.HtmlUri || current.NamespaceUri == NamespaceNames.SvgUri))
                {
                    language = current.GetAttribute(AttributeNames.Lang);
                }

                if (language is not null)
                {
                    return language;
                }
            }

            return element.Owner?.Context.GetLanguage();
        }
    }
}
