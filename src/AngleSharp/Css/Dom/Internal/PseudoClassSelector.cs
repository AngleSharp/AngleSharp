namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    sealed class PseudoClassSelector : ISelector
    {
        private readonly Predicate<IElement> _action;
        private readonly Func<IElement, IElement?, Boolean>? _actionWithScope;
        private readonly String _pseudoClass;

        public PseudoClassSelector(Predicate<IElement> action, String pseudoClass)
        {
            _action = action;
            _pseudoClass = pseudoClass;
            Specificity = Priority.OneClass;
        }

        public PseudoClassSelector(Predicate<IElement> action, String pseudoClass, Priority specificity)
        {
            _action = action;
            _pseudoClass = pseudoClass;
            Specificity = specificity;
        }

        public PseudoClassSelector(Func<IElement, IElement?, Boolean> action, String pseudoClass)
        {
            _action = _ => false;
            _actionWithScope = action;
            _pseudoClass = pseudoClass;
            Specificity = Priority.OneClass;
        }

        public Priority Specificity { get; }

        public String Text => PseudoClassNames.Separator + _pseudoClass;

        public void Accept(ISelectorVisitor visitor) => visitor.PseudoClass(_pseudoClass);

        public Boolean Match(IElement element, IElement? scope) => _actionWithScope?.Invoke(element, scope) ?? _action.Invoke(element);
    }
}
