namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A simple list of tokens that is immutable.
    /// </summary>
    class TokenList : ITokenList, IBindable
    {
        #region Fields

        readonly List<String> _tokens;

        #endregion

        #region Events

        public event Action<String> Changed;

        #endregion

        #region ctor

        internal TokenList(String value)
        {
            _tokens = new List<String>();
            Update(value);
        }

        #endregion

        #region Index

        public String this[Int32 index]
        {
            get { return _tokens[index]; }
        }

        #endregion

        #region Properties

        public Int32 Length
        {
            get { return _tokens.Count; }
        }

        #endregion

        #region Methods

        public void Update(String value)
        {
            _tokens.Clear();

            if (!String.IsNullOrEmpty(value))
            {
                var elements = value.SplitSpaces();

                for (var i = 0; i < elements.Length; i++)
                {
                    if (!_tokens.Contains(elements[i]))
                    {
                        _tokens.Add(elements[i]);
                    }
                }
            }
        }

        public Boolean Contains(String token)
        {
            return _tokens.Contains(token);
        }

        public void Remove(params String[] tokens)
        {
            var changed = false;

            foreach (var token in tokens)
            {
                if (_tokens.Contains(token))
                {
                    _tokens.Remove(token);
                    changed = true;
                }
            }

            if (changed)
            {
                RaiseChanged();
            }
        }

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
            {
                RaiseChanged();
            }
        }

        public Boolean Toggle(String token, Boolean force = false)
        {
            var contains = _tokens.Contains(token);

            if (contains && force)
            {
                return true;
            }

            if (contains)
            {
                _tokens.Remove(token);
            }
            else
            {
                _tokens.Add(token);
            }

            RaiseChanged();
            return !contains;
        }

        #endregion

        #region Helper

        void RaiseChanged()
        {
            Changed?.Invoke(ToString());
        }

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<String> GetEnumerator()
        {
            return _tokens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region String representation

        public override String ToString()
        {
            return String.Join(" ", _tokens);
        }

        #endregion
    }
}
