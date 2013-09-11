using AngleSharp.DOM;
using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    sealed class AttributeDeclaration : Node
    {
        List<AttributeDeclarationEntry> _attrs;

        internal AttributeDeclaration(IEnumerable<AttributeDeclarationEntry> attributes)
        {
            _attrs = new List<AttributeDeclarationEntry>(attributes);
        }

        public AttributeDeclarationEntry this[Int32 index]
        {
            get { return _attrs[index]; }
        }

        public Int32 Count
        {
            get { return _attrs.Count; }
        }

        public IEnumerable<AttributeDeclarationEntry> Declarations
        {
            get
            {
                foreach (var attribute in _attrs)
                    yield return attribute;
            }
        }

        public String Name
        {
            get;
            set;
        }

        public Boolean Check(Attr attribute)
        {
            foreach (var attr in _attrs)
            {
                if (attr.Name == attribute.Name)
                    return attr.Check(attribute);
            }

            return false;
        }
    }
}
