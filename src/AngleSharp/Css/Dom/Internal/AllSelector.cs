namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    sealed class AllSelector : ISelector
    {
        public static readonly ISelector Instance = new AllSelector();

        private AllSelector()
        {
        }

        public Priority Specificity => Priority.Zero;

        public String Text => "*";

        public void Accept(ISelectorVisitor visitor) => visitor.Type(Text);

        public Boolean Match(IElement element, IElement scope) => true;
    }
}
