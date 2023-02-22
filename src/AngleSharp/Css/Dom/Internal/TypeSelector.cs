namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    sealed class TypeSelector : ISelector
    {
        public TypeSelector(String type)
        {
            TypeName = type;
        }

        /// <summary>
        /// Gets the raw type name value
        /// </summary>
        internal String TypeName { get; }

        public Priority Specificity => Priority.OneTag;

        public String Text => CssUtilities.Escape(TypeName);

        public void Accept(ISelectorVisitor visitor) => visitor.Type(TypeName);

        public Boolean Match(IElement element, IElement? scope) => TypeName.Isi(element.LocalName);
    }
}
