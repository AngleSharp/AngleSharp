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

        public String Text => "." + CssUtilities.Escape(_cls);

        public void Accept(ISelectorVisitor visitor) => visitor.Class(_cls);

        public Boolean Match(IElement element, IElement? scope)
        {
            // Workaround for #1252 (Android AoT issues)
            var list = element.ClassList;

            if (list is TokenList concreteList)
            {
                return concreteList.Contains(_cls);
            }

            return list.Contains(_cls);
        }
    }
}
