namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    sealed class AttrAvailableSelector : BaseAttrSelector, ISelector
    {
        public AttrAvailableSelector(String name, String prefix)
            : base(name, prefix)
        {
        }

        public String Text => String.Concat("[", Attribute, "]");

        public void Accept(ISelectorVisitor visitor) => visitor.Attribute(Attribute, String.Empty, null);

        public Boolean Match(IElement element, IElement scope) => element.HasAttribute(Name);
    }
}
