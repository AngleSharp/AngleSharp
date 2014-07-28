namespace AngleSharp.DOM.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// A simple list of tokens that is immutable.
    /// </summary>
    class TokenList : ITokenList
    {
        #region Fields

        readonly List<String> _tokens;
        readonly Element _parent;
        readonly String _attribute;
        Boolean _blocking;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new list of tokens.
        /// </summary>
        internal TokenList(Element parent, String attribute)
        {
            _attribute = attribute;
            _parent = parent;
            _tokens = new List<String>();
            _blocking = false;
            Update(parent.GetAttribute(attribute));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of tokens.
        /// </summary>
        public Int32 Length
        {
            get { return _tokens.Count; }
        }

        /// <summary>
        /// Gets an item in the list by its index.
        /// </summary>
        /// <param name="index">The index of the token.</param>
        /// <returns>The token at the specified index.</returns>
        public String this[Int32 index]
        {
            get
            {
                var value = index >= 0 && index < _tokens.Count ? _tokens[index] : null;
                Debug.Assert(value != null, "The index you specified is out of range!");
                return value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns true if the underlying string contains token, otherwise false.
        /// </summary>
        /// <param name="token">The token to consider.</param>
        /// <returns>True if the string contained the token, otherwise false.</returns>
        public Boolean Contains(String token)
        {
            if(_tokens.Contains(token))
                return true;

            return false;
        }

        /// <summary>
        /// Removes tokens from the underlying string.
        /// </summary>
        /// <param name="tokens">The tokens to remove.</param>
        public void Remove(params String[] tokens)
        {
            var changed = false;

            foreach (var token in tokens)
            {
                if (_tokens.Remove(token))
                    changed = true;
            }

            if (changed)
                Propagate();
        }

        /// <summary>
        /// Adds tokens to the underlying string.
        /// </summary>
        /// <param name="tokens">The tokens to add.</param>
        public void Add(params String[] tokens)
        {
            var changed = false;

            foreach (var token in tokens)
            {
                if (!_tokens.Contains(token))
                {
                    _tokens.Add(token);
                    changed = true;
                }
            }

            if (changed)
                Propagate();
        }

        /// <summary>
        /// Removes token from string and returns false. If token doesn't exist it's added and the function returns true.
        /// </summary>
        /// <param name="token">The token to consider.</param>
        /// <param name="force">Forces the element to stay, if it is already added.</param>
        /// <returns>True if the string contained the token, otherwise false.</returns>
        public Boolean Toggle(String token, Boolean force = false)
        {
            var contains = _tokens.Contains(token);

            if (contains && force)
                return true;
            else if (contains)
                _tokens.Remove(token);
            else
                _tokens.Add(token);

            Propagate();
            return !contains;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Updates the DOMTokenList with the given value.
        /// </summary>
        /// <param name="value">The new value.</param>
        internal void Update(String value)
        {
            if (!_blocking)
            {
                _tokens.Clear();

                if (String.IsNullOrEmpty(value))
                    return;

                var elements = value.SplitSpaces();

                for (int i = 0; i < elements.Length; i++)
                {
                    if (!_tokens.Contains(elements[i]))
                        _tokens.Add(elements[i]);
                }
            }
        }

        #endregion

        #region Helper

        /// <summary>
        /// Sets the current value of the attribute.
        /// </summary>
        void Propagate()
        {
            _blocking = true;
            _parent.SetAttribute(_attribute, ToString());
            _blocking = false;
        }

        #endregion

        #region IEnumerable Implementation

        /// <summary>
        /// Returns an enumerator that iterates through the strings in the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the tokens.</returns>
        public IEnumerator<String> GetEnumerator()
        {
            return _tokens.GetEnumerator();
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
        public override String ToString()
        {
            return String.Join(" ", _tokens);
        }

        #endregion
    }
}
