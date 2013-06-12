using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// The character token that contains a series of characters.
    /// </summary>
    abstract class CssCharacterToken : CssToken
    {
        #region Members

        Char _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new character token.
        /// </summary>
        public CssCharacterToken()
        {
            _data = Specification.NULL;
        }

        /// <summary>
        /// Creates a new character token with the given character.
        /// </summary>
        /// <param name="data">The character.</param>
        public CssCharacterToken(Char data)
        {
            _data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data of the character token.
        /// </summary>
        public Char Data
        {
            get { return _data; }
        }

        #endregion
    }
}
