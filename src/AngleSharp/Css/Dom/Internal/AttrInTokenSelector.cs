namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    sealed class AttrInTokenSelector : BaseAttrSelector, ISelector
    {
        private readonly String _value;

        public AttrInTokenSelector(String name, String value, String prefix = null)
            : base(name, prefix)
        {
            _value = value;
        }

        public String Text
        {
            get
            {
                return String.Concat("[", Attribute, "|=", _value.CssString(), "]");
            }
        }

        public void Accept(ISelectorVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public Boolean Match(IElement element, IElement scope)
        {
            if (!String.IsNullOrEmpty(_value))
            {
                var actual = element.GetAttribute(Name) ?? String.Empty;
                return actual.HasHyphen(_value);
            }

            return false;
        }
    }
}
