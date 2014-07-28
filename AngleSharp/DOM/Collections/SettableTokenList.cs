namespace AngleSharp.DOM.Collections
{
    using System;

    /// <summary>
    /// A list of tokens that can be modified.
    /// </summary>
    sealed class SettableTokenList : TokenList, ISettableTokenList
    {
        #region ctor

        /// <summary>
        /// Creates a new list of tokens.
        /// </summary>
        internal SettableTokenList(Element parent, String attribute)
            : base(parent, attribute)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the token list.
        /// </summary>
        public String Value
        {
            get { return ToString(); }
            set { Update(value); }
        }

        #endregion
    }
}
