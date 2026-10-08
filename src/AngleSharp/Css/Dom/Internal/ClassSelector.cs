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
            var list = element.ClassList;

            if (element.Owner?.CompatMode == "BackCompat")
            {
                foreach (var token in list)
                {
                    if (CssUtilities.EqualsAsciiIgnoreCase(token, _cls))
                    {
                        return true;
                    }
                }

                return false;
            }

            // Workaround for #1252 (Android AoT issues)
            if (list is TokenList concreteList)
            {
                return concreteList.Contains(_cls);
            }

            return list.Contains(_cls);
        }
    }
}
