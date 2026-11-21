namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    sealed class PseudoClassSelector : ISelector
    {
        private readonly Predicate<IElement> _action;
        private readonly Func<IElement, IElement?, Boolean>? _actionWithScope;
        // Null until first read for a function such as :not() or :is(), whose text includes the
        // serialized argument: building it while parsing re-serialized every nested argument
        // once per enclosing function, for a text that matching never looks at.
        private String? _pseudoClass;
        private readonly String? _name;
        private readonly ISelector? _argument;

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

        /// <summary>
        /// Creates a functional pseudo-class, whose text is the name followed by the argument
        /// in parentheses (computed on first use).
        /// </summary>
        public PseudoClassSelector(Predicate<IElement> action, String name, ISelector argument, Priority specificity)
        {
            _action = action;
            _name = name;
            _argument = argument;
            Specificity = specificity;
        }

        /// <summary>
        /// Creates a functional pseudo-class with the specificity of a class, see above.
        /// </summary>
        public PseudoClassSelector(Predicate<IElement> action, String name, ISelector argument)
            : this(action, name, argument, Priority.OneClass)
        {
        }

        /// <summary>
        /// Creates a functional pseudo-class depending on the scope, see above.
        /// </summary>
        public PseudoClassSelector(Func<IElement, IElement?, Boolean> action, String name, ISelector argument)
        {
            _action = _ => false;
            _actionWithScope = action;
            _name = name;
            _argument = argument;
            Specificity = Priority.OneClass;
        }

        public Priority Specificity { get; }

        // Racing readers at worst build equal strings; the reference write is atomic.
        private String PseudoClass => _pseudoClass ??= _name!.CssFunction(_argument!.Text);

        public String Text => PseudoClassNames.Separator + PseudoClass;

        public void Accept(ISelectorVisitor visitor) => visitor.PseudoClass(PseudoClass);

        public Boolean Match(IElement element, IElement? scope) => _actionWithScope?.Invoke(element, scope) ?? _action.Invoke(element);
    }
}
