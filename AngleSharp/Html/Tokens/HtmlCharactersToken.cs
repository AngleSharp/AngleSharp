using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngleSharp.Html
{
    /// <summary>
    /// The character token that contains a series of characters.
    /// </summary>
    sealed class HtmlCharactersToken : HtmlToken
    {
        #region Members

        Char[] _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new characters token.
        /// </summary>
        public HtmlCharactersToken()
        {
            _data = new Char[0];
            _type = HtmlTokenType.Characters;
        }

        /// <summary>
        /// Creates a new characters token with the given characters.
        /// </summary>
        /// <param name="data">The characters.</param>
        public HtmlCharactersToken(String data)
        {
            _data = data.ToCharArray();
            _type = HtmlTokenType.Characters;
        }

        /// <summary>
        /// Creates a new characters token with the given characters.
        /// </summary>
        /// <param name="data">The characters.</param>
        public HtmlCharactersToken(Char[] data)
        {
            _data = data;
            _type = HtmlTokenType.Characters;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data of the character token.
        /// </summary>
        public Char[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Gets if the character data is NULL.
        /// </summary>
        /// <returns>True if the character token is NULL, otherwise false.</returns>
        public override Boolean IsNullChar
        {
            get { return false; }
        }

        /// <summary>
        /// Gets if the character data is a new line.
        /// </summary>
        /// <returns>True if the character token is a new line, otherwise false.</returns>
        public override Boolean IsNewLine
        {
            get { return false; }
        }

        /// <summary>
        /// Gets if the character data is actually a space character.
        /// </summary>
        /// <returns>True if the character data is a space character.</returns>
        public override Boolean IsIgnorable
        {
            get { return false; }
        }

        #endregion
    }
}
