using AngleSharp.DOM;
using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    sealed class AttributeEnumeratedType : AttributeTypeDeclaration
    {
        #region Members

        List<String> _names;

        #endregion

        #region ctor

        public AttributeEnumeratedType()
        {
            _names = new List<String>();
        }

        #endregion

        #region Properties

        public Boolean IsNotation
        {
            get;
            set;
        }

        public List<String> Names
        {
            get { return _names; }
        }

        #endregion

        #region Methods

        public override Boolean Check(Element element)
        {
            if (element.HasAttribute(Parent.Name))
            {
                var attr = element.Attributes[Parent.Name];

                if (!Names.Contains(attr.Value))
                    return false;
            }

            return true;
        }

        #endregion
    }
}
