namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    sealed class PseudoElementSelector : ISelector
    {
        private readonly Predicate<IElement> _action;
        private readonly String _pseudoElement;

        public PseudoElementSelector(Predicate<IElement> action, String pseudoElement)
        {
            _action = action;
            _pseudoElement = pseudoElement;
        }

        public Priority Specificity => Priority.OneTag;

        public String Text => PseudoElementNames.Separator + _pseudoElement;

        public void Accept(ISelectorVisitor visitor) => visitor.PseudoElement(_pseudoElement);

        public Boolean Match(IElement element, IElement scope) => _action.Invoke(element);
    }
}
