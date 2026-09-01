namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    sealed class AttrNotMatchSelector : BaseAttrSelector, ISelector
    {
        private readonly String _value;

        public AttrNotMatchSelector(String name, String value, String? prefix = null, Boolean insensitive = false)
            : base(name, prefix, insensitive)
        {
            _value = value;
        }

        public String Text => String.Concat("[", Attribute, "!=", _value.CssString(), Modifier, "]");

        public void Accept(ISelectorVisitor visitor) => visitor.Attribute(Attribute, "!=", _value);

        public Boolean Match(IElement element, IElement? scope) => !String.Equals(element.GetAttribute(Name), _value, Comparison);
    }
}
