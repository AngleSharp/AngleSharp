namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a pseudo-class taking a selector argument that it matches, or with negate
    /// does not match: :not(), :is(), :where() and :matches().
    /// </summary>
    /// <remarks>
    /// A PseudoClassSelector with a predicate did the same, at the cost of a closure and a
    /// delegate per function parsed and a delegate invocation per element matched.
    /// </remarks>
    sealed class ArgumentPseudoClassSelector : ISelector
    {
        private readonly String _name;
        private readonly ISelector _argument;
        private readonly Boolean _negate;
        // Built on first read, as for PseudoClassSelector
        private String? _code;

        public ArgumentPseudoClassSelector(String name, ISelector argument, Priority specificity, Boolean negate)
        {
            _name = name;
            _argument = argument;
            _negate = negate;
            Specificity = specificity;
        }

        public Priority Specificity { get; }

        public String Text => PseudoClassNames.Separator + Code;

        private String Code => _code ??= _name.CssFunction(_argument.Text);

        public void Accept(ISelectorVisitor visitor) => visitor.PseudoClass(Code);

        // The argument is matched against the document element as scope rather than the one
        // given, exactly as the predicate (via SelectorExtensions.Match) always did.
        public Boolean Match(IElement element, IElement? scope) => _argument.Match(element) != _negate;
    }
}
