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

        public Priority Specificity
        {
            get { return Priority.Zero; }
        }

        public String Text
        {
            get { return "*"; }
        }

        public void Accept(ISelectorVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public Boolean Match(IElement element, IElement scope)
        {
            return true;
        }
    }
}
