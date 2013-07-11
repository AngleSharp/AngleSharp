using System;
using System.Collections.Generic;

namespace AngleSharp.Xml
{
    sealed class XmlElementDeclarationToken : XmlBaseDeclarationToken
    {
        #region Members

        List<String> _names;
        ContentType _contentType;
        List<ElementDeclarationEntry> _children;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlElementDeclarationToken()
        {
            _type = XmlTokenType.ElementDeclaration;
            _children = new List<ElementDeclarationEntry>();
            _names = new List<String>();
        }

        #endregion

        #region Properties

        public ContentType CType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        public List<String> Names
        {
            get { return _names; }
        }

        public List<ElementDeclarationEntry> Children
        {
            get { return _children; }
        }

        #endregion

        #region Nested Enumerations

        public enum ContentType
        {
            Empty,
            Any,
            Mixed,
            Children
        }

        public enum ElementQuantifier
        {
            One,
            ZeroOrOne,
            ZeroOrMore,
            OneOrMore
        }

        #endregion

        #region Nested Classes

        public abstract class ElementDeclarationEntry
        {
            public ElementQuantifier Quantifier
            {
                get;
                set;
            }
        }

        public sealed class ElementNameDeclarationEntry
        {
            public String Name
            {
                get;
                set;
            }
        }

        public sealed class ElementChoiceDeclarationEntry
        {
            List<ElementDeclarationEntry> _choice;

            public ElementChoiceDeclarationEntry()
            {
                _choice = new List<ElementDeclarationEntry>();
            }

            public List<ElementDeclarationEntry> Choice
            {
                get { return _choice; }
            }
        }

        public sealed class ElementSequenceDeclarationEntry
        {
            List<ElementDeclarationEntry> _seq;

            public ElementSequenceDeclarationEntry()
            {
                _seq = new List<ElementDeclarationEntry>();
            }

            public List<ElementDeclarationEntry> Sequence
            {
                get { return _seq; }
            }
        }

        #endregion
    }
}
