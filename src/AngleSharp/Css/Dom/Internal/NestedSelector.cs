namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    sealed class NestedSelector : ISelector
    {
        public static readonly ISelector Instance = new NestedSelector();

        private NestedSelector()
        {
        }

        public Priority Specificity => Priority.OneClass;

        public String Text => "&";

        public void Accept(ISelectorVisitor visitor) => visitor.Type(Text);

        public Boolean Match(IElement element, IElement? scope) => element.Owner!.DocumentElement == element;
    }
}
