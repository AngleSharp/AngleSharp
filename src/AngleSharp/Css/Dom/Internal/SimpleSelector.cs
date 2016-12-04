namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents a simple selector (either a type selector, universal
    /// selector, attribute, class, id or pseudo-class selector).
    /// </summary>
    sealed class SimpleSelector : ISelector
    {
        #region Fields

        private readonly Predicate<IElement> _matches;
        private readonly Priority _specificity;
        private readonly String _code;

        #endregion

        #region ctor

        public SimpleSelector(Predicate<IElement> matches, Priority specifify, String code)
        {
            _matches = matches;
            _specificity = specifify;
            _code = code;
        }

        #endregion

        #region Properties

        public Priority Specificity
        {
            get { return _specificity; }
        }

        public String Text
        {
            get { return _code; }
        }

        #endregion

        #region Static constructors

        public static SimpleSelector PseudoElement(Predicate<IElement> action, String pseudoElement)
        {
            return new SimpleSelector(action, Priority.OneTag, PseudoElementNames.Separator + pseudoElement);
        }

        public static SimpleSelector PseudoClass(Predicate<IElement> action, String pseudoClass)
        {
            return new SimpleSelector(action, Priority.OneClass, PseudoClassNames.Separator + pseudoClass);
        }

        #endregion

        #region Methods

        public Boolean Match(IElement element, IElement scope)
        {
            return _matches.Invoke(element);
        }

        public void Accept(ISelectorVisitor visitor)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
