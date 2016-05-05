using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    abstract class AttributeTypeDeclaration
    {
        public AttributeDeclarationEntry Parent 
        { 
            get; 
            set; 
        }

        public abstract Boolean Check(Element element);
    }
}
