using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Represents a list of DOMTokens.
    /// </summary>
    public sealed class DOMTokenList : IHTMLObject, IEnumerable<String>
    {
        #region Members

        List<String> tokens;
        String oldTokenString;
        Func<String> getter;
        Action<String> setter;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new list of tokens.
        /// </summary>
        public DOMTokenList()
        {
            tokens = new List<string>();
        }

        /// <summary>
        /// Creates a bound DOMTokenList from the given properties.
        /// </summary>
        /// <param name="getter">The access to the getter property part.</param>
        /// <param name="setter">The access to the setter property part.</param>
        /// <returns>The DOMTokenList.</returns>
        internal static DOMTokenList From(Func<string> getter, Action<string> setter)
        {
            var list = new DOMTokenList();

            list.getter = getter;
            list.setter = setter;

            return list;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of tokens.
        /// </summary>
        public int Length
        {
            get
            {
                GetValue(); 
                return tokens.Count;
            }
        }

        /// <summary>
        /// Gets an item in the list by its index.
        /// </summary>
        /// <param name="index">The index of the token.</param>
        /// <returns>The token at the specified index.</returns>
        public string this[int index]
        {
            get
            {
                GetValue();

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
        public bool Contains(string token)
        {
            GetValue();

            if(tokens.Contains(token))
                return true;

            return false;
        }

        /// <summary>
        /// Returns true if the underlying string contains at least one of the tokens, otherwise false.
        /// </summary>
        /// <param name="tokens">The tokens to consider.</param>
        /// <returns>True if the string contained a token, otherwise false.</returns>
        internal bool Contains(string[] tokens)
        {
            GetValue();

            for (int i = 0; i < tokens.Length; i++)
                if (this.tokens.Contains(tokens[i]))
                    return true;

            return false;
        }

        /// <summary>
        /// Remove token from the underlying string.
        /// </summary>
        /// <param name="token">The token to remove.</param>
        /// <returns>The current token list.</returns>
        public DOMTokenList Remove(string token)
        {
            GetValue();
            tokens.Remove(token);
            SetValue();
            return this;
        }

        /// <summary>
        /// Adds token to the underlying string.
        /// </summary>
        /// <param name="token">The token to add.</param>
        /// <returns>The current token list.</returns>
        public DOMTokenList Add(string token)
        {
            GetValue();

            if (!tokens.Contains(token))
            {
                tokens.Add(token);
                SetValue();
            }

            return this;
        }

        /// <summary>
        /// Removes token from string and returns false. If token doesn't exist it's added and the function returns true.
        /// </summary>
        /// <param name="token">The token to consider.</param>
        /// <returns>True if the string contained the token, otherwise false.</returns>
        public bool Toggle(string token)
        {
            GetValue();

            if (tokens.Contains(token))
            {
                tokens.Remove(token);
                SetValue();
                return false;
            }

            tokens.Add(token);
            SetValue();
            return true;
        }

        #endregion

        #region Helper

        /// <summary>
        /// Gets the current value from the getter.
        /// </summary>
        void GetValue()
        {
            if (getter != null)
            {
                var str = getter();

                if (str != oldTokenString)
                {
                    tokens.Clear();
                    oldTokenString = str;
                    var strs = str.SplitSpaces();

                    for (int i = 0; i < strs.Length; i++)
                        if (!tokens.Contains(strs[i]))
                            tokens.Add(strs[i]);
                }
            }
        }

        /// <summary>
        /// Sets the currnt value using the setter.
        /// </summary>
        void SetValue()
        {
            oldTokenString = string.Join(" ", tokens);

            if (setter != null)
                setter(oldTokenString);
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
            return ((IEnumerable)tokens).GetEnumerator();
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the token list.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public String ToHtml()
        {
            return oldTokenString;
        }

        #endregion
    }
}
