﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    sealed class PseudoClassSelector : ISelector
    {
        private readonly Predicate<IElement> _action;
        private readonly String _pseudoClass;

        public PseudoClassSelector(Predicate<IElement> action, String pseudoClass)
        {
            _action = action;
            _pseudoClass = pseudoClass;
        }

        public Priority Specificity
        {
            get { return Priority.OneClass; }
        }

        public String Text
        {
            get { return PseudoClassNames.Separator + _pseudoClass; }
        }

        public void Accept(ISelectorVisitor visitor)
        {
            visitor.PseudoClass(_pseudoClass);
        }

        public Boolean Match(IElement element, IElement scope)
        {
            return _action.Invoke(element);
        }
    }
}
