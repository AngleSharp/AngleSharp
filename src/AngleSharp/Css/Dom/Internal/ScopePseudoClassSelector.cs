namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    sealed class ScopePseudoClassSelector : ISelector
    {
        public static readonly ISelector Instance = new ScopePseudoClassSelector();

        private ScopePseudoClassSelector()
        {
        }

        public Priority Specificity => Priority.OneClass;

        public String Text => PseudoClassNames.Separator + PseudoClassNames.Scope;

        public void Accept(ISelectorVisitor visitor) => visitor.PseudoClass(PseudoClassNames.Scope);

        public Boolean Match(IElement element, IElement scope)
        {
            var realScope = scope ?? element.Owner.DocumentElement;
            return Object.ReferenceEquals(element, realScope);
        }
    }
}
