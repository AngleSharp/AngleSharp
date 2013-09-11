using AngleSharp.DOM;
using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    sealed class AttributeDeclaration : Node
    {
        #region Members

        List<AttributeDeclarationEntry> _attrs;

        #endregion

        #region ctor

        internal AttributeDeclaration(IEnumerable<AttributeDeclarationEntry> attributes)
        {
            _attrs = new List<AttributeDeclarationEntry>(attributes);
        }

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        public Boolean Check(Element element)
        {
            if (element.Attributes.Length > _attrs.Count)
                return false;

            for (int i = 0; i < element.Attributes.Length; i++)
            {
                var contains = false;

                foreach (var attr in _attrs)
                {
                    if (attr.Name == element.Attributes[i].Name)
                    {
                        contains = true;
                        break;
                    }
                }

                if (!contains)
                    return false;
            }

            foreach (var attr in _attrs)
                return attr.Check(element);

            return true;
        }

        #endregion
    }
}
