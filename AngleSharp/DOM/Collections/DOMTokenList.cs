using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Represents a list of DOMTokens.
    /// </summary>
    [DOM("DOMTokenList")]
    public sealed class DOMTokenList : IHTMLObject, IEnumerable<String>
    {
        #region Members

        List<String> tokens;
        Element parent;
        Boolean blocking;
        String attribute;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new list of tokens.
        /// </summary>
        internal DOMTokenList(Element parent, String attribute)
        {
            this.attribute = attribute;
            this.parent = parent;
            this.tokens = new List<String>();
            this.blocking = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of tokens.
        /// </summary>
        [DOM("length")]
        public Int32 Length
        {
            get { return tokens.Count; }
        }

        /// <summary>
        /// Gets an item in the list by its index.
        /// </summary>
        /// <param name="index">The index of the token.</param>
        /// <returns>The token at the specified index.</returns>
        [DOM("item")]
        public String this[Int32 index]
        {
            get
            {
                if (index < 0 || index >= tokens.Count)
                    return null;

                return tokens[index];
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns true if the underlying string contains token, otherwise false.
        /// </summary>
        /// <param name="token">The token to consider.</param>
        /// <returns>True if the string contained the token, otherwise false.</returns>
        [DOM("contains")]
        public Boolean Contains(String token)
        {
            if(tokens.Contains(token))
                return true;

            return false;
        }

        /// <summary>
        /// Remove token from the underlying string.
        /// </summary>
        /// <param name="token">The token to remove.</param>
        /// <returns>The current token list.</returns>
        [DOM("remove")]
        public DOMTokenList Remove(String token)
        {
            tokens.Remove(token);
            Propagate();
            return this;
        }

        /// <summary>
        /// Adds token to the underlying string.
        /// </summary>
        /// <param name="token">The token to add.</param>
        /// <returns>The current token list.</returns>
        [DOM("add")]
        public DOMTokenList Add(String token)
        {
            if (!tokens.Contains(token))
            {
                tokens.Add(token);
                Propagate();
            }

            return this;
        }

        /// <summary>
        /// Removes token from string and returns false. If token doesn't exist it's added and the function returns true.
        /// </summary>
        /// <param name="token">The token to consider.</param>
        /// <returns>True if the string contained the token, otherwise false.</returns>
        [DOM("toggle")]
        public Boolean Toggle(String token)
        {
            var contains = tokens.Contains(token);

            if (contains)
                tokens.Remove(token);
            else
                tokens.Add(token);

            Propagate();
            return !contains;
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Updates the DOMTokenList with the given value.
        /// </summary>
        /// <param name="value">The new value.</param>
        internal void Update(String value)
        {
            if (!blocking)
            {
                tokens.Clear();
                var elements = value.SplitSpaces();

                for (int i = 0; i < elements.Length; i++)
                    if (!tokens.Contains(elements[i]))
                        tokens.Add(elements[i]);
            }
        }

        /// <summary>
        /// Returns true if the underlying string contains at least one of the tokens, otherwise false.
        /// </summary>
        /// <param name="tokens">The tokens to consider.</param>
        /// <returns>True if the string contained a token, otherwise false.</returns>
        internal Boolean Contains(String[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++)
                if (this.tokens.Contains(tokens[i]))
                    return true;

            return false;
        }

        #endregion

        #region Helper

        /// <summary>
        /// Sets the current value of the attribute.
        /// </summary>
        void Propagate()
        {
            blocking = true;
            parent.SetAttribute(attribute, ToHtml());
            blocking = false;
        }

        #endregion

        #region IEnumerable implementation

        /// <summary>
        /// Returns an enumerator that iterates through the strings in the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the tokens.</returns>
        public IEnumerator<String> GetEnumerator()
        {
            return tokens.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the strings in the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the tokens.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the token list.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public String ToHtml()
        {
            return String.Join(" ", tokens);
        }

        #endregion
    }
}
