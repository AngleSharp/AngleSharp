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
            return new XmlCommentToken(data);
        }

        /// <summary>
        /// Creates a new doctype token.
        /// </summary>
        /// <returns>The created token.</returns>
        public static XmlDoctypeToken Doctype()
        {
            return new XmlDoctypeToken();
        }

        /// <summary>
        /// Creates a new declaration token.
        /// </summary>
        /// <returns>The created token.</returns>
        public static XmlDeclarationToken Declaration()
        {
            return new XmlDeclarationToken();
        }

        /// <summary>
        /// Creates a new character token.
        /// </summary>
        /// <param name="character">The character data.</param>
        /// <returns>The created token.</returns>
        public static XmlCharacterToken Character(Char character)
        {
            return new XmlCharacterToken(character);
        }

        /// <summary>
        /// Creates a new open tag token.
        /// </summary>
        /// <returns>The created token.</returns>
        public static XmlTagToken OpenTag()
        {
            return new XmlTagToken { _type = XmlTokenType.StartTag };
        }

        /// <summary>
        /// Creates a new close tag token.
        /// </summary>
        /// <returns>The created token.</returns>
        public static XmlTagToken CloseTag()
        {
            return new XmlTagToken { _type = XmlTokenType.EndTag };
        }

        /// <summary>
        /// Creates a new processing instruction token.
        /// </summary>
        /// <returns>The created token.</returns>
        public static XmlPIToken Processing()
        {
            return new XmlPIToken();
        }

        /// <summary>
        /// Creates a new CData token.
        /// </summary>
        /// <param name="data">The raw data.</param>
        /// <returns>The created token.</returns>
        public static XmlCDataToken CData(String data)
        {
            return new XmlCDataToken(data);
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
