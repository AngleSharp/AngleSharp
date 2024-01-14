namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    sealed class NestedSelector : INestedSelector
    {
        private ISelector _parent;

        public NestedSelector(ISelector parent)
        {
            _parent = parent;
        }

        public ISelector ParentSelector { get => _parent; set => _parent = value; }

        public Priority Specificity => _parent?.Specificity ?? Priority.Zero;

        public String Text => "&";

        public void Accept(ISelectorVisitor visitor) => visitor.Type(Text);

        public Boolean Match(IElement element, IElement? scope) => _parent?.Match(element, scope) ?? false;
    }
}
