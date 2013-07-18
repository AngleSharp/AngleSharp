using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class ElementDeclaration : Node
    {
        /// <summary>
        /// Zero-Order approximation.
        /// </summary>
        DtdElementToken _token;

        internal ElementDeclaration(DtdElementToken token)
        {
            _token = token;
        }

        public ElementDeclarationEntry.ContentType Type
        {
            get { return _token.CType; }
        }

        public String Name
        {
            get { return _token.Name; }
        }

        public ElementDeclarationEntry Entry
        {
            get { return _token.Entry; }
        }
    }
}
