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

        public Priority Specificity => Priority.OneClass;

        public String Text => "." + _cls;

        public void Accept(ISelectorVisitor visitor) => visitor.Class(_cls);

        public Boolean Match(IElement element, IElement scope) => element.ClassList.Contains(_cls);
    }
}
