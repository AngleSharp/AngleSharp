using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    abstract class AttributeValueDeclaration
    {
        public AttributeDeclarationEntry Parent 
        { 
            get; 
            set; 
        }

        public abstract Boolean Apply(Element element);
    }
}
