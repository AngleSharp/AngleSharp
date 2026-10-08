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
            var quirks = CssUtilities.IsInQuirksMode(element);

            // Workaround for #1252 (Android AoT issues)
            if (list is TokenList concreteList)
            {
                if (!quirks)
                {
                    return concreteList.Contains(_cls);
                }

                for (var i = 0; i < concreteList.Length; i++)
                {
                    if (CssUtilities.EqualsAsciiIgnoreCase(concreteList[i], _cls))
                    {
                        return true;
                    }
                }

                return false;
            }

            if (!quirks)
            {
                return list.Contains(_cls);
            }

            for (var i = 0; i < list.Length; i++)
            {
                if (CssUtilities.EqualsAsciiIgnoreCase(list[i], _cls))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
