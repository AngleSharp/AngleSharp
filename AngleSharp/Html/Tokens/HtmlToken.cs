using System;
using System.Diagnostics;

namespace AngleSharp.Html
{
    /// <summary>
    /// The abstract base class of any HTML token.
    /// </summary>
    [DebuggerStepThrough]
    abstract class HtmlToken
    {
        #region Factory

        /// <summary>
        /// Gets the end of file token.
        /// </summary>
        public static HtmlEndOfFileToken EOF
        {
            get { return eof ?? (eof = new HtmlEndOfFileToken()); }
        }

        /// <summary>
        /// Creates a new HTML character token based on the given character.
        /// </summary>
        /// <param name="character">The character to contain.</param>
        /// <returns>The generated token.</returns>
        [DebuggerStepThrough]
        public static HtmlCharacterToken Character(Char character)
        {
            return new HtmlCharacterToken(character);
        }

        /// <summary>
        /// Creates a new HTML characters token based on the given characters.
        /// </summary>
        /// <param name="character1">The first character to contain.</param>
        /// <param name="character2">The second character to contain.</param>
        /// <returns>The generated token.</returns>
        [DebuggerStepThrough]
        public static HtmlCharactersToken Characters(Char character1, Char character2)
        {
            return new HtmlCharactersToken(new Char[] { character1, character2 });
        }

        /// <summary>
        /// Creates a new HTML characters token based on the given characters.
        /// </summary>
        /// <param name="characters">The characters to contain.</param>
        /// <returns>The generated token.</returns>
        [DebuggerStepThrough]
        public static HtmlCharactersToken Characters(Char[] characters)
        {
            return new HtmlCharactersToken(characters);
        }

        /// <summary>
        /// Creates a new HTML characters token based on the given characters.
        /// </summary>
        /// <param name="characters">The characters to contain.</param>
        /// <returns>The generated token.</returns>
        [DebuggerStepThrough]
        public static HtmlCharactersToken Characters(String characters)
        {
            return new HtmlCharactersToken(characters);
        }

        /// <summary>
        /// Creates a new HTML comment token based on the given string.
        /// </summary>
        /// <param name="comment">The comment to contain.</param>
        /// <returns>The generated token.</returns>
        [DebuggerStepThrough]
        public static HtmlCommentToken Comment(String comment)
        {
            return new HtmlCommentToken(comment);
        }

        /// <summary>
        /// Creates a new HTML doctype token.
        /// </summary>
        /// <param name="quirksmode">Determines if quirksmode will be forced.</param>
        /// <returns>The generated token.</returns>
        [DebuggerStepThrough]
        public static HtmlDoctypeToken Doctype(Boolean quirksmode)
        {
            return new HtmlDoctypeToken(quirksmode);
        }

        /// <summary>
        /// Creates a new opening HtmlTagToken.
        /// </summary>
        /// <returns>The new HTML tag token.</returns>
        [DebuggerStepThrough]
        public static HtmlTagToken OpenTag()
        {
            return new HtmlTagToken { _type = HtmlTokenType.StartTag };
        }

        /// <summary>
        /// Creates a new closing HtmlTagToken.
        /// </summary>
        /// <returns>The new HTML tag token.</returns>
        [DebuggerStepThrough]
        public static HtmlTagToken CloseTag()
        {
            return new HtmlTagToken { _type = HtmlTokenType.EndTag };
        }

        /// <summary>
        /// Creates a new opening HtmlTagToken for the given name.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <returns>The new HTML tag token.</returns>
        [DebuggerStepThrough]
        public static HtmlTagToken OpenTag(String name)
        {
            return new HtmlTagToken(name) { _type = HtmlTokenType.StartTag };
        }

        /// <summary>
        /// Creates a new closing HtmlTagToken for the given name.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <returns>The new HTML tag token.</returns>
        [DebuggerStepThrough]
        public static HtmlTagToken CloseTag(String name)
        {
            return new HtmlTagToken(name) { _type = HtmlTokenType.EndTag };
        }

        #endregion

        #region Members

        static HtmlEndOfFileToken eof;
        protected HtmlTokenType _type;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        public HtmlTokenType Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets if the character data is NULL.
        /// </summary>
        /// <returns>True if the character token is NULL, otherwise false.</returns>
        public virtual Boolean IsNullChar
        {
            get { return false; }
        }

        /// <summary>
        /// Gets if the character data is a new line.
        /// </summary>
        /// <returns>True if the character token is a new line, otherwise false.</returns>
        public virtual Boolean IsNewLine
        {
            get { return false; }
        }

        /// <summary>
        /// Gets if the character data is actually a space character.
        /// </summary>
        /// <returns>True if the character data is a space character.</returns>
        public virtual Boolean IsIgnorable
        {
            get { return false; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Finds out if the current token is a start tag token with the given name.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <returns>True if the token is indeed a start tag token with the given name, otherwise false.</returns>
        public virtual Boolean IsStartTag(String name)
        {
            return false;
        }

        #endregion
    }
}
