using System;
using System.Diagnostics;

namespace AngleSharp.Xml
{
    /// <summary>
    /// The abstract base class of any XML token.
    /// </summary>
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

        /// <summary>
        /// Creates a new comment token.
        /// </summary>
        /// <param name="data">The data in the comment.</param>
        /// <returns>The created token.</returns>
        public static XmlToken Comment(String data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new doctype token.
        /// </summary>
        /// <returns>The created token.</returns>
        public static XmlDoctypeToken Doctype()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new declaration token.
        /// </summary>
        /// <returns>The created token.</returns>
        public static XmlDeclarationToken Declaration()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new character token.
        /// </summary>
        /// <param name="character">The character data.</param>
        /// <returns>The created token.</returns>
        public static XmlCharacterToken Character(Char character)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new string / characters token.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>The created token.</returns>
        public static XmlToken Characters(String value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new string / characters token.
        /// </summary>
        /// <param name="head">The first character to insert.</param>
        /// <param name="tail">The second character to store.</param>
        /// <returns>The created token.</returns>
        public static XmlToken Characters(Char head, Char tail)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new string / characters token.
        /// </summary>
        /// <param name="value">The character array.</param>
        /// <returns>The created token.</returns>
        public static XmlToken Characters(Char[] value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new open tag token.
        /// </summary>
        /// <returns>The created token.</returns>
        public static XmlTagToken OpenTag()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new close tag token.
        /// </summary>
        /// <returns>The created token.</returns>
        public static XmlTagToken CloseTag()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new processing instruction token.
        /// </summary>
        /// <returns>The created token.</returns>
        public static XmlPIToken Processing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new CData token.
        /// </summary>
        /// <param name="data">The raw data.</param>
        /// <returns>The created token.</returns>
        public static XmlCDataToken CData(String data)
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
