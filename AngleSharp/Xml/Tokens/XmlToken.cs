using System;
using System.Diagnostics;

namespace AngleSharp.Xml
{
    [DebuggerStepThrough]
    abstract class XmlToken
    {
        #region Members

        static XmlEndOfFileToken eof;
        protected XmlTokenType _type;

        #endregion

        #region Factory

        /// <summary>
        /// Gets the end of file token.
        /// </summary>
        public static XmlEndOfFileToken EOF
        {
            get { return eof ?? (eof = new XmlEndOfFileToken()); }
        }

        public static XmlToken Comment(String p)
        {
            throw new NotImplementedException();
        }

        public static XmlDoctypeToken Doctype(Boolean p)
        {
            throw new NotImplementedException();
        }

        public static XmlDeclaration Declaration()
        {
            throw new NotImplementedException();
        }

        public static XmlCharacterToken Character(Char p)
        {
            throw new NotImplementedException();
        }

        public static XmlToken Characters(String value)
        {
            throw new NotImplementedException();
        }

        public static XmlToken Characters(Char head, Char tail)
        {
            throw new NotImplementedException();
        }

        public static XmlToken Characters(Char[] value)
        {
            throw new NotImplementedException();
        }

        public static XmlTagToken OpenTag()
        {
            throw new NotImplementedException();
        }

        public static XmlTagToken CloseTag()
        {
            throw new NotImplementedException();
        }

        public static XmlPIToken Processing()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        public XmlTokenType Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets if the token is a character token and contains a
        /// white-space character.
        /// </summary>
        public virtual Boolean IsIgnorable
        {
            get { return false; }
        }

        #endregion
    }
}
