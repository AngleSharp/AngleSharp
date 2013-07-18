using System;
using System.Collections.Generic;

namespace AngleSharp.Xml
{
    sealed class XmlElementDeclaration : XmlBaseDeclaration
    {
        #region Members

        List<String> _names;
        ContentType _contentType;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlElementDeclaration()
        {
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

        public ElementQuantifier Quantifier
        {
            get;
            set;
        }

        public ElementDeclarationEntry Entry
        {
            get;
            set;
        }

        #endregion

        #region Nested Enumerations

        public enum ContentType
        {
            /// <summary>
            /// EMPTY
            /// </summary>
            Empty,
            /// <summary>
            /// ANY
            /// </summary>
            Any,
            /// <summary>
            /// (#PCDATA|name|name|...)*
            /// </summary>
            Mixed,
            /// <summary>
            /// ((a,b,(c?,d*),(e|f+))?,g)+
            /// </summary>
            Children
        }

        public enum ElementQuantifier
        {
            /// <summary>
            /// Nothing specified.
            /// </summary>
            One,
            /// <summary>
            /// Questionmark specified.
            /// </summary>
            ZeroOrOne,
            /// <summary>
            /// Asterisk specified.
            /// </summary>
            ZeroOrMore,
            /// <summary>
            /// Plus specified.
            /// </summary>
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

        public sealed class ElementNameDeclarationEntry : ElementDeclarationEntry
        {
            public String Name
            {
                get;
                set;
            }
        }

        public sealed class ElementChoiceDeclarationEntry : ElementDeclarationEntry
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

        public sealed class ElementSequenceDeclarationEntry : ElementDeclarationEntry
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
