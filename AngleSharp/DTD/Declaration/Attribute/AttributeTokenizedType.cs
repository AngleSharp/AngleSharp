using AngleSharp.DOM;
using AngleSharp.Xml;
using System;

namespace AngleSharp.DTD
{
    sealed class AttributeTokenizedType : AttributeTypeDeclaration
    {
        #region Properties

        public TokenizedType Value
        {
            get;
            set;
        }

        #endregion

        #region Enumeration

        public enum TokenizedType
        {
            ID,
            IDREF,
            IDREFS,
            ENTITY,
            ENTITIES,
            NMTOKEN,
            NMTOKENS
        }

        #endregion

        #region Methods

        public override Boolean Check(Element element)
        {
            var attr = element.GetAttribute(Parent.Name);

            if (attr == null)
                return true;

            switch (Value)
            {
                case TokenizedType.ENTITIES:
                {
                    //TODO
                    break;
                }
                case TokenizedType.ENTITY:
                {
                    //TODO
                    break;
                }
                case TokenizedType.ID:
                {
                    if (String.IsNullOrEmpty(attr) || !attr[0].IsXmlNameStart())
                        return false;

                    for (int i = 1; i < attr.Length; i++)
                        if (!attr[i].IsXmlName())
                            return false;

                    //TODO only one ID per element
                    return true;
                }
                case TokenizedType.IDREF:
                {
                    if (String.IsNullOrEmpty(attr) || !attr[0].IsXmlNameStart())
                        return false;

                    for (int i = 1; i < attr.Length; i++)
                        if (!attr[i].IsXmlName())
                            return false;

                    //TODO check reference
                    return true;
                }
                case TokenizedType.IDREFS:
                {
                    var start = true;

                    for (int i = 0; i < attr.Length; i++)
                    {
                        if (!attr[i].IsSpaceCharacter())
                        {
                            if (start && !attr[i].IsXmlNameStart())
                                return false;
                            else if (!start && !attr[i].IsXmlName())
                                return false;
                            else if (start)
                                start = false;
                        }
                        else
                            start = true;
                    }

                    //TODO check references
                    return true;
                }
                case TokenizedType.NMTOKEN:
                {
                    for (int i = 0; i < attr.Length; i++)
                        if (!attr[i].IsXmlName())
                            return false;

                    return true;
                }
                case TokenizedType.NMTOKENS:
                {
                    for (int i = 0; i < attr.Length; i++)
                        if (!attr[i].IsSpaceCharacter() && !attr[i].IsXmlName())
                            return false;

                    break;
                }
            }

            return true;
        }

        #endregion
    }
}
