namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    sealed class AttrNotMatchSelector : BaseAttrSelector, ISelector
    {
        private readonly String _name;
        private readonly String _value;

        public AttrNotMatchSelector(String name, String value, String prefix = null)
            : base(name, prefix)
        {
            _value = value;
        }

        public String Text
        {
            get { return String.Concat("[", Attribute, "!=", _value.CssString(), "]"); }
        }

        public void Accept(ISelectorVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public Boolean Match(IElement element, IElement scope)
        {
            return !element.GetAttribute(Name).Is(_value);
        }
    }
}
