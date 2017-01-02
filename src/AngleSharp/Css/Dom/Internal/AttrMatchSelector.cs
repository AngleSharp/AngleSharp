﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    sealed class AttrMatchSelector : BaseAttrSelector, ISelector
    {
        private readonly String _value;

        public AttrMatchSelector(String name, String value, String prefix = null)
            : base(name, prefix)
        {
            _value = value;
        }

        public String Text
        {
            get { return String.Concat("[", Attribute, "=", _value.CssString(), "]"); }
        }

        public void Accept(ISelectorVisitor visitor)
        {
            visitor.Attribute(Attribute, "=", _value);
        }

        public Boolean Match(IElement element, IElement scope)
        {
            return element.GetAttribute(Name).Is(_value);
        }
    }
}
