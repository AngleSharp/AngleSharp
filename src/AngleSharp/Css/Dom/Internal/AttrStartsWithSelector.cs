namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    sealed class AttrStartsWithSelector : BaseAttrSelector, ISelector
    {
        private readonly String _value;

        public AttrStartsWithSelector(String name, String value, String? prefix = null, Boolean insensitive = false)
            : base(name, prefix, insensitive)
        {
            _value = value;
        }

        public String Text => String.Concat("[", Attribute, "^=", _value.CssString(), Modifier, "]");

        public void Accept(ISelectorVisitor visitor) => visitor.Attribute(Attribute, "^=", _value);

        public Boolean Match(IElement element, IElement? scope)
        {
            if (!String.IsNullOrEmpty(_value))
            {
                var actual = element.GetAttribute(Name) ?? String.Empty;
                return actual.StartsWith(_value, Comparison);
            }

            return false;
        }
    }
}
