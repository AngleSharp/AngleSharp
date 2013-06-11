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

        String _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new character token.
        /// </summary>
        public HtmlCharactersToken()
        {
            _data = String.Empty;
            _type = HtmlTokenType.Characters;
        }

        /// <summary>
        /// Creates a new character token with the given character.
        /// </summary>
        /// <param name="data">The character.</param>
        public HtmlCharactersToken(String data)
        {
            _data = data;
            _type = HtmlTokenType.Characters;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data of the character token.
        /// </summary>
        public String Data
        {
            get { return _data; }
        }

        /// <summary>
        /// Gets if the character data is NULL.
        /// </summary>
        /// <returns>True if the character token is NULL, otherwise false.</returns>
        public override Boolean IsNullChar
        {
            get { return _data.Length == 1 && _data[0] == Specification.NULL; }
        }

        /// <summary>
        /// Gets if the character data is actually a space character.
        /// </summary>
        /// <returns>True if the character data is a space character.</returns>
        public override Boolean IsIgnoreable
        {
            get { return false; }
        }

        #endregion
    }
}
