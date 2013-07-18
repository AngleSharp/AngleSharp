using AngleSharp.DOM;
using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    sealed class AttributeDeclaration : Node
    {
        /// <summary>
        /// Zero-Order approximation.
        /// </summary>
        DtdAttributeToken _token;

        internal AttributeDeclaration(DtdAttributeToken token)
        {
            _token = token;
        }

        public AttributeDeclarationEntry this[Int32 index]
        {
            get { return _token.Attributes[index]; }
        }

        public Int32 Count
        {
            get { return _token.Attributes.Count; }
        }

        public IEnumerable<AttributeDeclarationEntry> Declarations
        {
            get
            {
                foreach (var attribute in _token.Attributes)
                    yield return attribute;
            }
        }

        public String Name 
        {
            get { return _token.Name; }
        }
    }
}
