using System;

namespace AngleSharp.Html
{
    /// <summary>
    /// The abstract base class of any token.
    /// </summary>
    abstract class HtmlToken
    {
        #region Factory

        /// <summary>
        /// Creates a new HTML character token based on the given string.
        /// </summary>
        /// <param name="characters">The characters to contain.</param>
        /// <returns>The generated token.</returns>
        public static HtmlToken Characters(string characters)
        {
            return new HtmlCharacterToken(characters);
        }

        #endregion

        #region Members

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

        #endregion

        #region Methods

        /// <summary>
        /// Looks up if the token is a character token with NULL being the character data.
        /// </summary>
        /// <returns>True if the passed token is a character token and contains NULL, otherwise false.</returns>
        public bool IsNullChar
        {
            get
            {
                return Type == HtmlTokenType.Character && ((HtmlCharacterToken)this).Data == Specification.NULL;
            }
        }

        /// <summary>
        /// Looks up if the token is a character token and if the data is actually a space character.
        /// </summary>
        /// <returns>True if the token is a CharacterToken and the data of the
        /// character token is a space character.</returns>
        public bool IsIgnoreable
        {
            get
            {
                return Type == HtmlTokenType.Character && Specification.IsSpaceCharacter(((HtmlCharacterToken)this).Data);
            }
        }

        #endregion
    }
}
