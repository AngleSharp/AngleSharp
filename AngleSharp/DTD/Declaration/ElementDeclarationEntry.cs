using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    abstract class ElementDeclarationEntry
    {
        public ElementQuantifier Quantifier
        {
            get;
            set;
        }

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
    }

    sealed class ElementMixedDeclarationEntry : ElementDeclarationEntry
    {
        List<String> _names;

        public ElementMixedDeclarationEntry()
        {
            _names = new List<String>();
        }

        public List<String> Names
        {
            get { return _names; }
        }
    }

    sealed class ElementNameDeclarationEntry : ElementDeclarationEntry
    {
        public String Name
        {
            get;
            set;
        }
    }

    sealed class ElementChoiceDeclarationEntry : ElementDeclarationEntry
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

    sealed class ElementSequenceDeclarationEntry : ElementDeclarationEntry
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
}
