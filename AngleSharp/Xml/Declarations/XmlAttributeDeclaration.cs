using System;
using System.Collections.Generic;

namespace AngleSharp.Xml
{
    sealed class XmlAttributeDeclaration : XmlBaseDeclaration
    {
        #region Members

        List<AttributeDeclaration> _attributes;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlAttributeDeclaration()
        {
            _attributes = new List<AttributeDeclaration>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of attributes defined.
        /// </summary>
        public List<AttributeDeclaration> Attributes
        {
            get { return _attributes; }
        }

        #endregion

        #region Nested Classes

        public sealed class AttributeDeclaration
        {
            public String Name
            {
                get;
                set;
            }

            public TypeDeclaration ValueType
            {
                get;
                set;
            }

            public ValueDeclaration ValueDefault
            {
                get;
                set;
            }
        }

        public abstract class ValueDeclaration
        {
        }

        public sealed class RequiredValueDeclaration : ValueDeclaration
        {

        }

        public sealed class ImpliedValueDeclaration : ValueDeclaration
        {

        }

        public sealed class CustomValueDeclaration : ValueDeclaration
        {
            public Boolean IsFixed
            {
                get;
                set;
            }

            public String Value
            {
                get;
                set;
            }
        }

        public abstract class TypeDeclaration
        {
        }

        public sealed class StringTypeDeclaration : TypeDeclaration
        {
        }

        public sealed class TokenizedTypeDeclaration : TypeDeclaration
        {
            public TokenizedType Value
            {
                get;
                set;
            }
        }

        public sealed class EnumeratedTypeDeclaration : TypeDeclaration
        {
            List<String> _names;

            public EnumeratedTypeDeclaration()
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
    }
}
