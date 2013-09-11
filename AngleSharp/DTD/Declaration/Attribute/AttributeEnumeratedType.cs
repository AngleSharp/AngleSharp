using AngleSharp.DOM;
using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    sealed class AttributeEnumeratedType : AttributeTypeDeclaration
    {
        List<String> _names;

        public AttributeEnumeratedType()
        {
            _names = new List<String>();
        }

        public Boolean IsNotation
        {
            get;
            set;
        }

        public List<String> Names
        {
            get { return _names; }
        }

        public override Boolean Check(Element element)
        {
            return true;
        }
    }
}
