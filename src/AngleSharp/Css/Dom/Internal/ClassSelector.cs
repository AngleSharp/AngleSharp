namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    sealed class ClassSelector : ISelector
    {
        private readonly String _cls;

        public ClassSelector(String cls)
        {
            _cls = cls;
        }

        public Priority Specificity
        {
            get { return Priority.OneClass; }
        }

        public String Text
        {
            get { return "." + _cls; }
        }

        public void Accept(ISelectorVisitor visitor)
        {
            visitor.Class(_cls);
        }

        public Boolean Match(IElement element, IElement scope)
        {
            return element.ClassList.Contains(_cls);
        }
    }
}
