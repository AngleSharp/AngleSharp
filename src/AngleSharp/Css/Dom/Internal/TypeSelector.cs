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

        public Priority Specificity => Priority.OneTag;

        public String Text => _type;

        public void Accept(ISelectorVisitor visitor) => visitor.Type(_type);

        public Boolean Match(IElement element, IElement scope) => _type.Isi(element.LocalName);
    }
}
