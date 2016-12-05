namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    sealed class TypeSelector : ISelector
    {
        private readonly String _type;

        public TypeSelector(String type)
        {
            _type = type;
        }

        public Priority Specificity
        {
            get { return Priority.OneTag; }
        }

        public String Text
        {
            get { return _type; }
        }

        public void Accept(ISelectorVisitor visitor)
        {
            visitor.Type(_type);
        }

        public Boolean Match(IElement element, IElement scope)
        {
            return _type.Isi(element.LocalName);
        }
    }
}
